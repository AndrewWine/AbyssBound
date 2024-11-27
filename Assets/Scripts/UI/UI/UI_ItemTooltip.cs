using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ItemTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTypeText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private void Start()
    {
        gameObject.SetActive(false);
    }

    public void ShowToolTip(ItemData_equipment data)
    {
        if (data == null)
        {
          
            return;
        }

        itemNameText.text = data.itemName;
        itemTypeText.text = data.itemtype.ToString();
        itemDescription.text = data.GetDescription();
        

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }

}
