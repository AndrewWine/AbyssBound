using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    public  static Action<ItemData> NotifyEquipItem;
    public static Action<ItemData> NotifyRemoveItem;

    private UI ui;
    public InventoryItem item;

    private void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemImage.color = Color.white;

        if (item != null)
        {
            itemImage.sprite = item.data.icon;
            itemText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
        }
        else
        {
            itemImage.sprite = null;
            itemText.text = "";
        }
    }

    public void CleanUpSlot()
    {
        item = null;
        itemImage.sprite = null ;
        itemImage.color = Color.clear;
        itemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item.data != null) // Kiểm tra item và item.data
        {
            if (item.data.itemtype == ItemType.Equipment)
            {
                NotifyEquipItem?.Invoke(item.data);
                Debug.Log("Trang bi Item");
                //CleanUpSlot();
            }
            else
            {
                Debug.Log("Chưa trang bị Item");
            }

            if (Input.GetKey(KeyCode.LeftControl))
            {
                NotifyRemoveItem?.Invoke(item.data);
            }
        }
        else
        {
            // Xử lý khi item hoặc item.data là null
            Debug.LogWarning("Item hoặc ItemData không hợp lệ.");
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer Enter Called");

        if (ui == null)
        {
            Debug.Log("UI không được gán!");
            return;
        }

        if (ui.itemTooltip == null)
        {
            Debug.Log("Item Tooltip không được gán trong UI!");
            return;
        }

        if (item == null || item.data == null)
        {
            Debug.Log("Item hoặc Item.data là null!");
            return;
        }

        if (item.data is ItemData_equipment equipmentData)
        {
            Debug.Log($"Hiển thị tooltip cho item: {equipmentData.itemName}");
            ui.itemTooltip.ShowToolTip(equipmentData);
        }
        else
        {
            Debug.Log("Item.data không phải là kiểu ItemData_equipment.");
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
            return;
        ui.itemTooltip.HideToolTip(); 
    }
}
