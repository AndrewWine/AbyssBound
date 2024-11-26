using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private GameObject craftSlotPrefab;
    [SerializeField] private GameObject craftWindow;
    [SerializeField] private List<ItemData_equipment> craftEquipment;
    [SerializeField] private List<UI_CraftSlot> craftSlots;
    void Start()
    {
        craftWindow.SetActive(false);

        AssingCraftSlots();
    }

    private void AssingCraftSlots()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
            craftSlots.Add(craftSlotParent.GetChild(i).GetComponent<UI_CraftSlot>());
        }
    }
    
    public void SetupCraftList()
    {
        for (int i = 0; i< craftSlots.Count; i++)
        {
            Destroy(craftSlots[i].gameObject);

        }
        craftSlots = new List<UI_CraftSlot> ();

        for( int i = 0; i< craftEquipment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftSlotPrefab, craftSlotParent);
            newSlot.GetComponent<UI_CraftSlot>().SetupCraftSlot(craftEquipment[i]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
        SetupCraftList();
        if (craftSlotPrefab != null)
            craftWindow.SetActive(true);
        else craftWindow.SetActive(false); ;
    }
}
