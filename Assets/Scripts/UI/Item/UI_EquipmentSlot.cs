using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class UI_EquipmentSlot : UI_ItemSlot
{
    public static Action<ItemData> NotifyUnequipItem;
    public EquipmentType slotType;

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
    }

    private void OnValidate()
    {
        gameObject.name = "Equipment slot -" + slotType.ToString();
        NotifyUnequipItem?.Invoke(item.data);

    }
}
