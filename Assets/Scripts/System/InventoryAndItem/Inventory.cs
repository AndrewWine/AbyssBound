using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour, ISaveManager
{
    public List<InventoryItem> equipment;
    public Dictionary<ItemData_equipment, InventoryItem> equipmentDictionory;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionory;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;
    public List<ItemData> startingItems;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;

    [SerializeField]public CharacterStats characterStats;
    [SerializeField] public PlayerManager playerManager;

    //Oserver
    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;
    private UI_ItemSlot NotifyEquipItem;
    private UI_ItemSlot NotifyRemoveItem;
    public Action UpdateStats;

    [Header("data base")]
    public List<InventoryItem> loadedItems;
    public List<ItemData_equipment> loadEquipment;
    private void Awake()
    {
        inventory = new List<InventoryItem>();
        inventoryDictionory = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDictionary = new Dictionary<ItemData, InventoryItem>();
        stashItemSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();

        equipment = new List<InventoryItem>();
        equipmentDictionory = new Dictionary<ItemData_equipment, InventoryItem>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EquipmentSlot>();
        characterStats = GetComponent<CharacterStats>();
        characterStats = GetComponent<CharacterStats>();
        if (characterStats == null)
        {
            Debug.LogError("CharacterStats is not assigned in Inventory.");
            return;
        }
    }


    private void Start()
    {
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        AddStartingItems();
    }

    private void OnEnable()
    {
        ItemData.getItem += AddItem;
        UI_ItemSlot.NotifyEquipItem += EquipItem;
        UI_EquipmentSlot.NotifyUnequipItem += UnEquipItem;
        UI_CraftSlot.NotifyCraftingItem += CanCraft;  // Đăng ký vào sự kiện
        UI_ItemSlot.NotifyRemoveItem += RemoveItem;
        UI_CraftWindow.NotifyCraftingItem += CanCraft;
    }

    private void OnDisable()
    {
        ItemData.getItem -= AddItem;
        UI_ItemSlot.NotifyEquipItem -= EquipItem;
        UI_EquipmentSlot.NotifyUnequipItem -= UnEquipItem;
        UI_CraftSlot.NotifyCraftingItem -= CanCraft;  // Hủy đăng ký sự kiện
        UI_ItemSlot.NotifyRemoveItem -= RemoveItem;
        UI_CraftWindow.NotifyCraftingItem -= CanCraft;

    }


    private void UpdateSlotUI()
    {
        for (int i = 0; i <equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemData_equipment, InventoryItem> item in equipmentDictionory)
            {
                if (item.Key.equipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0;  i < inventoryItemSlot.Length; i++)
        {
            inventoryItemSlot[i].CleanUpSlot();
        }

        for(int i = 0;i < stashItemSlot.Length; i++)
        {
            stashItemSlot[i].CleanUpSlot();
        }




        for (int i = 0; i < inventory.Count; i++)
        {
            if (i < inventoryItemSlot.Length)
            {
                inventoryItemSlot[i].UpdateSlot(inventory[i]);
            }
        }

        for (int i = 0; i < stash.Count; i++)
        {
            if (i < stashItemSlot.Length)
            {
                stashItemSlot[i].UpdateSlot(stash[i]);
            }
        }
     
    }

   

    public void AddItem(ItemData item)
    {
        if (item.itemtype == ItemType.Equipment && CanAddItem())
        {
            AddToInventory(item);
        }
        else if (item.itemtype == ItemType.Material)
        {
            AddToStash(item);
        }

        UpdateSlotUI();
    }


    private void ApplyItemStats(ItemData_equipment item, bool isEquipping)
    {
        int multiplier = isEquipping ? 1 : -1;
        
        Debug.Log("Applying stats: " + item.name + " with multiplier: " + multiplier);

        // Reset the stats to the base value (can be retrieved from the character or item)
        characterStats.OnChangeMaxHP((int)(item.vitallity * multiplier ));
        characterStats.OnChangeMaxStamina((int)(item.vitallity * 3 * multiplier));
        characterStats.OnChangeMaxMana((int)(item.intelligence * multiplier));
        characterStats.OnChangeDamage((int)((item.Damage + item.strength) * multiplier));  // Cast to int
        characterStats.OnChangeMagicDamage((int)((item.MagicDamage + item.intelligence * 2) * multiplier));  // Cast to int
        characterStats.OnChangeCritChance(item.CritChance * multiplier);
        characterStats.OnChangeCritPower(item.CritPower * multiplier);
        characterStats.OnChangeArmor((int)(item.armor * multiplier));  // Cast to int
        characterStats.OnChangeMagicArmor((int)(item.magicArmor * multiplier));  // Cast to int
        characterStats.OnChangeEvasion();
        characterStats.OnChangeMovementSpeed(item.movementSpeed * multiplier);
        characterStats.OnchangeSTRENGTH((int)(item.strength  * multiplier));
        characterStats.OnchangeAGILITY((int)(item.agility * multiplier));
        characterStats.OnchangeINTELLIGENCE((int)(item.intelligence * multiplier));
        characterStats.OnchangeVITALITY((int)(item.vitallity * multiplier));

        // Additional effects
        characterStats.OnChangeCanIgnite(item.canIgnite * multiplier);
        characterStats.OnChangeCanFreeze(item.canFreaze * multiplier);
        characterStats.OnChangeCanShock(item.canShock * multiplier);
        characterStats.OnChangeHpRegenRate(item.hpRegenRate * multiplier);
        characterStats.OnChangeManaRegenRate(item.manaRegenRate * multiplier);
        characterStats.OnChangeStaminaRegenRate(item.staminaRegenRate * multiplier);
        UpdateStats?.Invoke();
    }




    // Hàm trang bị item
    public void EquipItem(ItemData _item)
    {
        // Kiểm tra nếu vật phẩm là loại Equipment
        if (_item is ItemData_equipment newEquipment)
        {
            InventoryItem newItem = new InventoryItem(newEquipment);

            // Kiểm tra xem có trang bị nào cùng loại (equipmentType) đang được sử dụng không
            ItemData_equipment oldEquipment = null;

            foreach (KeyValuePair<ItemData_equipment, InventoryItem> item in equipmentDictionory)
            {
                if (item.Key.equipmentType == newEquipment.equipmentType)
                {
                    oldEquipment = item.Key;
                    break; // Tìm thấy trang bị cũ, không cần kiểm tra thêm
                }
            }

            // Nếu có trang bị cũ, gỡ nó ra
            if (oldEquipment != null)
            {
                UnEquipItem(oldEquipment);
            }

            // Thêm trang bị mới vào danh sách
            equipment.Add(newItem);
            equipmentDictionory[newEquipment] = newItem;

            // Giảm số lượng vật phẩm trong inventory
            if (inventoryDictionory.TryGetValue(_item, out InventoryItem inventoryItem))
            {
                inventoryItem.RemoveStack(); // Giảm số lượng vật phẩm
                if (inventoryItem.stackSize <= 0)
                {
                    inventory.Remove(inventoryItem); // Xóa vật phẩm khỏi inventory nếu số lượng = 0
                    inventoryDictionory.Remove(_item);
                }
              
            }

            // Áp dụng chỉ số của trang bị mới
            ApplyItemStats(newEquipment, true);
            Debug.Log("Da them chi so");
            // Cập nhật giao diện
            UpdateSlotUI();
        }
    }


    // Hàm gỡ trang bị
    public void UnEquipItem(ItemData_equipment itemToRemove)
    {
        // Kiểm tra xem itemToRemove có tồn tại trong từ điển không
        if (equipmentDictionory.TryGetValue(itemToRemove, out InventoryItem value))
        {
            // Hủy bỏ các chỉ số của trang bị
            ApplyItemStats(itemToRemove, false);
            Debug.Log("Da xoa chi so");
            // Xóa trang bị khỏi danh sách và từ điển
            equipment.Remove(value);
            equipmentDictionory.Remove(itemToRemove);

            // Thêm lại trang bị vào inventory
            AddItem(itemToRemove); // Lưu ý: Chỉ thêm lại vào inventory, không gọi EquipItem

            // Cập nhật giao diện
            UpdateSlotUI();
        }
    }




    private void AddToStash(ItemData item)
    {
        if (stashDictionary.TryGetValue(item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(item);
            stash.Add(newItem);
            stashDictionary.Add(item, newItem);
        }
    }
    
    public void RemoveItem(ItemData item)
    {
        if (inventoryDictionory.TryGetValue(item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                inventory.Remove(value);
                inventoryDictionory.Remove(item);
            }
            else
                value.RemoveStack();
        }

        if (stashDictionary.TryGetValue(item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDictionary.Remove(item);
            }
            else
            {
                stashValue.RemoveStack();
            }
        }

        UpdateSlotUI();
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDictionory.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventory.Add(newItem);
            inventoryDictionory.Add(_item, newItem);
        }
    }

    public bool CanAddItem()
    {
        if (inventory.Count >= inventoryItemSlot.Length)
        {
            Debug.Log("No more space");
            return false;  // Trả về false khi không đủ chỗ
        }
        return true;  // Nếu có chỗ trống, trả về true
    }


    // Phương thức kiểm tra khả năng chế tạo
    public void CanCraft(ItemData _itemToCraft, List<InventoryItem> _requireMaterials)
    {
        List<InventoryItem> materialsToRemove = new List<InventoryItem>();

        // Kiểm tra nguyên liệu cần thiết
        for (int i = 0; i < _requireMaterials.Count; i++)
        {
            if (stashDictionary.TryGetValue(_requireMaterials[i].data, out InventoryItem stashvalue))
            {
                if (stashvalue.stackSize < _requireMaterials[i].stackSize)
                {
                    // Nếu không đủ nguyên liệu, trả về false
                    Debug.Log("Not enough materials");
                }
                else
                {
                    // Nếu đủ nguyên liệu, thêm vào danh sách để xóa
                    materialsToRemove.Add(stashvalue);
                    AddToInventory(_itemToCraft); // Thêm vật phẩm mới vào kho

                    Debug.Log("Here is your item: " + _itemToCraft.name);

                }
            }
            else
            {
                // Nếu không tìm thấy nguyên liệu trong kho
                Debug.Log("Not enough materials");
            }
        }

        // Nếu tất cả nguyên liệu có sẵn, tiến hành xóa và chế tạo vật phẩm
        for (int i = 0; i < materialsToRemove.Count; i++)
        {
            RemoveItem(materialsToRemove[i].data); // Xóa nguyên liệu
        }

        // Thông báo thành công chế tạo vật phẩm
    }

    private void AddStartingItems()
    {
        foreach(ItemData_equipment item in loadEquipment)
        {
            EquipItem(item);
        }

        if(loadedItems.Count > 0)
        {
            foreach(InventoryItem item in loadedItems)
            {
                for(int i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.data);
                }
            }
            return;

        }
        for (int i = 0; i < startingItems.Count; i++)
        {
            if (startingItems[i] != null)
                AddItem(startingItems[i]);
        }
    }

    public void LoadData(GameData _data)
    {
        foreach(KeyValuePair<string,int> pair in _data.inventory)
        {
            foreach(var item in GetItemDataBase())
            {
                if(item != null && item.itemId == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;
                    loadedItems.Add(itemToLoad);
                }
            }
        }

        foreach(string loadedItemId in _data.equipmentID)
        {
            foreach(var item in GetItemDataBase())
            {
                if(item != null && loadedItemId == item.itemId)
                {
                    loadEquipment.Add(item as ItemData_equipment);
                }
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        foreach(KeyValuePair<ItemData, InventoryItem> pair in inventoryDictionory)
        {
            _data.inventory.Add(pair.Key.itemId,pair.Value.stackSize);
        }

        foreach(KeyValuePair<ItemData,InventoryItem> pair in stashDictionary)
        {
            _data.inventory.Add(pair.Key.itemId, pair.Value.stackSize);
        }

        foreach(KeyValuePair<ItemData_equipment,InventoryItem> pair in equipmentDictionory)
        { 
            _data.equipmentID.Add(pair.Key.itemId);
        }
    }
    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Scripts/System/InventoryAndItem/Item" });

        foreach (string SOName in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(SOName);
            var itemData = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemData);
        }
        return itemDataBase;
    }
}