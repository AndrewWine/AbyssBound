using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class UI_CraftSlot : UI_ItemSlot
{
    public static Action<ItemData, List<InventoryItem>> NotifyCraftingItem; // Thay đổi Action để nhận List<InventoryItem>

    private void OnEnable()
    {
        UpdateSlot(item);
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemData itemData = item.data;  // Lấy ItemData từ item
        ItemData_equipment CraftData = item.data as ItemData_equipment;

        // Kiểm tra CraftData có phải là ItemData_equipment và chứa craftingMaterials
        if (CraftData != null)
        {
            List<InventoryItem> craftingMaterials = CraftData.craftingMaterials;

            // Invoke NotifyCraftingItem với đúng tham số
            NotifyCraftingItem?.Invoke(itemData, craftingMaterials);  // Truyền ItemData và List<InventoryItem>
        }
    }

    // Xử lý kết quả khi chế tạo thành công hoặc thất bại

}


