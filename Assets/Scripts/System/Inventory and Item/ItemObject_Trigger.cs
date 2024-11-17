using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision detected with: " + collision.gameObject.name);

        var player = collision.gameObject.GetComponent<Player>();
        Debug.Log("Is Player? " + (player != null));

        if (myItemObject == null)
        {
            Debug.LogWarning("myItemObject is null!");
        }

        if (player != null && myItemObject != null)
        {
            Debug.Log("PickupItem triggered!");
            myItemObject.PickupItem();
        }
    }


}
