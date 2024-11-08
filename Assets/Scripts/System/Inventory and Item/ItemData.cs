using UnityEngine;
using System;

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemtype;
    public string itemName;
    public Sprite icon;
    public static Action<ItemData> getItem;

    public void PickUpItem()
    {
        getItem?.Invoke(this);
    }
}
