using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSTM : MonoBehaviour
{
    public PlayerData PlayerData;


    private void Awake()
    {
        PlayerData.CurrentStamina = PlayerData.MaxStamina;
    }

  
    public void OnCurrentStaminaChange(int amount)
    {
        PlayerData.CurrentStamina += amount;
    }

    public void OnChangeMaxStamina(int amount)
    {
        if (PlayerData.MaxStamina > 0)
        {
            PlayerData.MaxMana += amount;
            if (PlayerData.MaxStamina < PlayerData.CurrentStamina)
            {
                PlayerData.CurrentStamina = PlayerData.MaxStamina;
            }

        }
    }
}
