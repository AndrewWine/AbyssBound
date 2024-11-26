using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class UI_CraftSlot : UI_ItemSlot
{
    public static Action<ItemData, List<InventoryItem>> NotifyCraftingItem; // Thay đổi Action để nhận List<InventoryItem>

    public void SetupCraftSlot(ItemData_equipment _data)
    {
        if (_data == null || itemImage == null || itemText == null)
            return;

        item.data = _data;
        itemImage.sprite = _data.icon;
        itemText.text = _data.itemName;

        if (itemText.text.Length > 12)
            itemText.fontSize = itemText.fontSize * 0.7f;
        else 
            itemText.fontSize = 16;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetupCraftWindow(item.data as ItemData_equipment);
    }

    protected override void Start()
    {
        base.Start();
    }

    // Xử lý kết quả khi chế tạo thành công hoặc thất bại

}


