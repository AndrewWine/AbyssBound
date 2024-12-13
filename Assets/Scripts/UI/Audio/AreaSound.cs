using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class AreaSound : MonoBehaviour
{
    public Action<int,Transform> soundAction;
    public Action<int> stopSoundAction;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
            soundAction?.Invoke(30,null);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            stopSoundAction?.Invoke(30);
    }
}
