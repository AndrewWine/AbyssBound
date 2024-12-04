using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int amountOfItems;
    [SerializeField] private ItemData[] possibleItemDrop;
    private List<ItemData> dropList = new List<ItemData>();
    [SerializeField] private GameObject dropPrefab;
    [SerializeField] private ItemData item;

    public void GenerateDrop()
    {
        // Đảm bảo AbyssEssence luôn có mặt trong drop list
        dropList.Clear();  // Làm sạch danh sách trước khi thêm vật phẩm mới

        // Thêm các vật phẩm có khả năng rơi vào danh sách
        for (int i = 0; i < possibleItemDrop.Length; i++)
        {
            if (Random.Range(0, 100) <= possibleItemDrop[i].dropChance)
                dropList.Add(possibleItemDrop[i]);
        }

        // Kiểm tra nếu danh sách trống
        if (dropList.Count == 0)
        {
            Debug.LogWarning("Không có vật phẩm nào được thêm vào danh sách. possibleDropList đang trống.");
            return; // Dừng nếu không có vật phẩm nào để rơi.
        }

        // Sinh các vật phẩm rơi
        for (int i = 0; i < amountOfItems; i++)
        {
            ItemData randomItem = dropList[Random.Range(0, dropList.Count)];  // Chọn vật phẩm ngẫu nhiên
            DropItem(randomItem);  // Drop vật phẩm
        }
    }

    public void DropItem(ItemData _itemdata)
    {
        if (_itemdata == null)
        {
            Debug.LogError("Vật phẩm không hợp lệ, không thể rơi!");
            return;
        }

        GameObject newDrop = Instantiate(dropPrefab, transform.position, Quaternion.identity);
        Vector2 randomVelocity = new Vector2(Random.Range(-5, 5), Random.Range(15, 25));
        newDrop.GetComponent<ItemObject>().SetupItem(_itemdata, randomVelocity);
    }
}
