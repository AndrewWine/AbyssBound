using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemText;
    public  static Action<ItemData> NotifyEquipItem;
    public InventoryItem item;

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
    public void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item.data.itemtype == ItemType.Equipment)
        {
            NotifyEquipItem?.Invoke(item.data);
        }
        else if(item == null)
        {
            Debug.Log("Chưa trang bị Item");
        } 
            
    }
}
