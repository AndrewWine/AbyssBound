using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private GameObject characterUI;

    public UI_ItemTooltip itemTooltip;

    private void Awake()
    {
        if (itemTooltip == null)
        {
            // Tự động tìm GameObject có Item_Tooltip
            itemTooltip = FindObjectOfType<UI_ItemTooltip>();

            if (itemTooltip == null)
            {
                Debug.Log("Không tìm thấy Item Tooltip trong scene!");
            }
            else
            {
                Debug.Log("Item Tooltip đã được tự động gán.");
            }
        }
    }

    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }
    }
}
    