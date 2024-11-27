using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image itemImage;
    [SerializeField] protected TextMeshProUGUI itemText;
    public  static Action<ItemData> NotifyEquipItem;
    public static Action<ItemData> NotifyRemoveItem;

    protected UI ui;
    public InventoryItem item;

    protected virtual void Start()
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
        if(item == null) return;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            NotifyRemoveItem?.Invoke(item.data);
            return;
        }

        if (item.data.itemtype == ItemType.Equipment)
        {
            NotifyEquipItem?.Invoke(item.data);
            Debug.Log("Trang bi Item");
        }
        ui.itemTooltip.HideToolTip();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null) return;
        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;
       

        if (mousePosition.x > 600)
            xOffset = -145;
        else
            xOffset = -145;

       
        ui.itemTooltip.ShowToolTip(item.data as ItemData_equipment);
        ui.itemTooltip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + -35);
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        if (item != null)
            return;
        ui.itemTooltip.HideToolTip(); 
    }
}
