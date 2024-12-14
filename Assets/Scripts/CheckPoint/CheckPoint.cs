using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class CheckPoint : MonoBehaviour
{
    private Animator anim;
    public string id;
    public bool activationStatus;

    public Action NotifySaveGame;

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
        if (collision.GetComponent<Player>() != null)
        {
            ActivateCheckpoint();
        }
    }

    public void ActivateCheckpoint()
    {
        NotifySaveGame?.Invoke();
        activationStatus = true;
        anim.SetBool("active", true);
    
    }
}
