using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public string id;
    public bool activationStatus;

    public static Action NotifySaveGameatCheckPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        SaveManager.resetPlayerData += GenerateID;
    }

    private void OnDisable()
    {
        SaveManager.resetPlayerData -= GenerateID;
    }

    [ContextMenu("Generate checkpoint id")]
    private void GenerateID()
    {
        id = System.Guid.NewGuid().ToString();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.GetComponent<Player>();
        if (player != null)
        {
            ActivateCheckpoint();
        }
        else
        {
            Debug.LogWarning("Collision does not have a Player component.");
        }
    }


    public void ActivateCheckpoint()
    {
        if (NotifySaveGameatCheckPoint != null)
        {
            NotifySaveGameatCheckPoint.Invoke();
        }
        else
        {
            Debug.LogWarning("No subscribers for NotifySaveGameatCheckPoint.");
        }

        activationStatus = true;

        if (anim != null)
        {
            anim.SetBool("active", true);
        }
        else
        {
            Debug.LogError("Animator is not assigned in CheckPoint.");
        }
    }

}
