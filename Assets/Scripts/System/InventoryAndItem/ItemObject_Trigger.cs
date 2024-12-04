using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject_Trigger : MonoBehaviour
{
    private ItemObject myItemObject => GetComponentInParent<ItemObject>();

    private void OnCollisionEnter2D(Collision2D collision)
    {

        var player = collision.gameObject.GetComponent<Player>();

        if (myItemObject == null)
        {
            Debug.LogWarning("myItemObject is null!");
        }

        if (player != null && myItemObject != null)
        {
            myItemObject.PickupItem();
        }
    }


}
