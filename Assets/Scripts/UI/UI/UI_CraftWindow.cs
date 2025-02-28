﻿using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;
    [SerializeField] private Button craftButton;

    [SerializeField] private Image[] materialImage;

    public static Action<ItemData, List<InventoryItem>> NotifyCraftingItem; // Thay đổi Action để nhận List<InventoryItem>

  

    public void SetupCraftWindow(ItemData_equipment _data)
    {
        craftButton.onClick.RemoveAllListeners();
      
        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0; i < _data.craftingMaterials.Count; i++)
        {
            if (_data.craftingMaterials.Count > materialImage.Length)
                Debug.LogWarning("You have more materials amount than you have material slots in craft window");
            
            materialImage[i].sprite = _data.craftingMaterials[i].data.icon;
            materialImage[i].color = Color.white;

            TextMeshProUGUI materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();

           materialSlotText.text = _data.craftingMaterials[i].stackSize.ToString();
            materialSlotText.color = Color.white;
            

            itemIcon.sprite = _data.icon;
            itemName.text = _data.name;
            itemDescription.text = _data.GetDescription();

            craftButton.onClick.AddListener(() =>
            {
                NotifyCraftingItem?.Invoke(_data, _data.craftingMaterials); // Gọi sự kiện với ItemData và danh sách nguyên liệu
            });
        }
    }
}
