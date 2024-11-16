using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public static Action<ItemData_equipment> NotifyUnequipItem;
    public EquipmentType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item != null && item.data is ItemData_equipment equipmentData)
        {
            NotifyUnequipItem?.Invoke(equipmentData);
            NotifyEquipItem?.Invoke(item.data);
            Debug.Log("Thao trang bi");
            CleanUpSlot();
        }
    }

    private void OnValidate()
    {
        gameObject.name = "Equipment slot -" + slotType.ToString();
    }
}
