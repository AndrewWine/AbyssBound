using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class UI_EquipmentSlot : UI_ItemSlot
{
    public static Action<ItemData> NotifyUnequipItem;
    public Action<ItemData> NotifyAddToInventory;
    public EquipmentType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        if (item != null && item.data != null)
        {
            NotifyUnequipItem?.Invoke(item.data);
            CleanUpSlot();
           
        }
    }


    private void OnValidate()
    {
        gameObject.name = "Equipment slot -" + slotType.ToString();
   
       
    }
}
