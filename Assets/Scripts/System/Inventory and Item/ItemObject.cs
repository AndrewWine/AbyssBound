using UnityEngine;

public class ItemObject : MonoBehaviour
{
    private SpriteRenderer sr;
    [SerializeField] private ItemData itemData;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object - " + itemData.name;
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            itemData.PickUpItem();  // Kích hoạt sự kiện thêm vào inventory
            Debug.Log("Picked up item " + itemData.itemName);
            Destroy(gameObject);  // Xóa object sau khi nhặt
        }
    }
}
