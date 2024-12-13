using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "ItemDatabase", menuName = "Data/Item Database")]
public class ItemDatabase : ScriptableObject
{
    public List<ItemData> itemDataList = new List<ItemData>();
}
