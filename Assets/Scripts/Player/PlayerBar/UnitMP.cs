using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMP : MonoBehaviour
{
    public PlayerData PlayerData;
    private void Awake()
    {
        PlayerData.CurrentMana = PlayerData.MaxMana;
    }

    public void OnCurrentManaChange(int amount)
    {
        PlayerData.CurrentMana += amount;
    }



    public void OnChangeMaxMana(int amount)
    {
        if (PlayerData.MaxMana > 0)
        {
            PlayerData.MaxMana += amount;
            if (PlayerData.MaxMana < PlayerData.CurrentMana)
            {
                PlayerData.CurrentMana = PlayerData.MaxMana;
            }

        }
    }

  
}
