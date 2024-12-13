using System;
using UnityEngine;

public class DeathBringerArea : MonoBehaviour
{
    public Action<int> bosssoundAction;
    public Action stopbossSoundAction;
    public GameObject deathBringerHealthBar;

    private void OnEnable()
    {
        DeathStateDeathBringer.removeHealthBar += DisableHealbarUI;
    }
    private void OnDisable()
    {
        DeathStateDeathBringer.removeHealthBar -= DisableHealbarUI;

    }

    public void DisableHealbarUI()
    {
        deathBringerHealthBar.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            Debug.Log("Player entered the trigger");
            bosssoundAction?.Invoke(12);
            deathBringerHealthBar.SetActive(true);

        }
        else
        {
            Debug.Log("Not a player, no sound triggered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
            stopbossSoundAction?.Invoke();
    }
}
