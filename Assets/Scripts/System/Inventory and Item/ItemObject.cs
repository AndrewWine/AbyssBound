using UnityEngine;

public class ItemObject : MonoBehaviour
{
    [SerializeReference] private Rigidbody2D rb;
    [SerializeField] private ItemData itemData;
    private void SetupVisuals()
    {
        if(itemData == null)
        {
            return;
        }
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = "Item object - " + itemData.name;
 
    }
     
   

    public void SetupItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.velocity = _velocity;

        SetupVisuals();
    }
    public void PickupItem()
    {
        itemData.PickUpItem();  // Kích hoạt sự kiện thêm vào inventory
        Debug.Log("Picked up item " + itemData.itemName);
        Destroy(gameObject);  // Xóa object sau khi nhặt
    }
}
