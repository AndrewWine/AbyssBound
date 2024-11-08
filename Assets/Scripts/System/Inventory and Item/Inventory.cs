using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Inventory : MonoBehaviour
{
    public List<InventoryItem> equipment;
    public Dictionary<ItemData_equipment, InventoryItem> equipmentDictionory;

    public List<InventoryItem> inventory;
    public Dictionary<ItemData, InventoryItem> inventoryDictionory;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDictionary;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlotParent;
    [SerializeField] private Transform stashSlotParent;
    [SerializeField] private Transform equipmentSlotParent;


    private UI_ItemSlot[] inventoryItemSlot;
    private UI_ItemSlot[] stashItemSlot;
    private UI_EquipmentSlot[] equipmentSlot;


    private UI_ItemSlot NotifyEquipItem;

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

    }

    private void Start()
    {
        inventoryItemSlot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    private void OnEnable()
    {
        ItemData.getItem += AddItem;
        UI_ItemSlot.NotifyEquipItem += EquipItem;
    }

    private void OnDisable()
    {
        ItemData.getItem -= AddItem;
        UI_ItemSlot.NotifyEquipItem -= EquipItem;
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
        if (item.itemtype == ItemType.Equipment)
        {
            AddToInventory(item);
        }
        else if (item.itemtype == ItemType.Material)
        {
            AddToStash(item);
        }

        UpdateSlotUI();
    }

    private void EquipItem(ItemData _item)
    {
        ItemData_equipment newEquipment = _item as ItemData_equipment;
        if (newEquipment != null)
        {
            InventoryItem newItem = new InventoryItem(newEquipment);

            // Kiểm tra và tháo bỏ item cũ nếu có
            ItemData_equipment oldEquipment = null;

            foreach (KeyValuePair<ItemData_equipment, InventoryItem> item in equipmentDictionory)
            {
                if (item.Key.equipmentType == newEquipment.equipmentType)
                {
                    oldEquipment = item.Key;
                }
            }

            if (oldEquipment != null)
            {
                UnEquipItem(oldEquipment);
                AddItem(oldEquipment);
            }

            // Thêm item mới vào equipment
            equipment.Add(newItem);
            equipmentDictionory[newEquipment] = newItem;
            RemoveItem(_item);

            UpdateSlotUI();
        }
    }


    private void UnEquipItem(ItemData_equipment itemToRemove)
    {
        if (equipmentDictionory.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDictionory.Remove(itemToRemove);
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
}