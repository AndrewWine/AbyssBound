using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerInputHandler inputHandler;
    private void Awake()
    {
       playerData.CurrentMana = playerData.MaxMana;
       playerData.CurrentStamina = playerData.MaxStamina;
       inputHandler.UseStamina += OnCurrentStaminaChange;
       inputHandler.UseMana += OnCurrentManaChange;
    }

    private void OnDisable()
    {
        inputHandler.UseStamina -= OnCurrentStaminaChange;
        inputHandler.UseMana -= OnCurrentManaChange;
    }

    public void OnCurrentManaChange(int amount)
    {
        playerData.CurrentMana += amount;
    }
    public void OnCurrentStaminaChange(int amount)
    {
        playerData.CurrentStamina += amount;
    }


    public void OnChangeMaxMana()
    {
        if (playerData.MaxMana > 0)
        {
            playerData.MaxMana += 2 * playerData.intelligence;
            if (playerData.MaxMana < playerData.CurrentMana)
            {
                playerData.CurrentMana = playerData.MaxMana;
            }

        }
    }

    public void OnChangeMaxStamina()
    {
        if (playerData.MaxStamina > 0)
        {
            playerData.MaxStamina += 3 * playerData.vitallity;
            if (playerData.MaxStamina < playerData.CurrentStamina)
            {
                playerData.CurrentStamina = playerData.MaxStamina;
            }

        }
    }

    public void OnChangeDamage()
    {
        playerData.Damage += playerData.strength;
    }

    public void OnChangeMagicDamage()
    {
        playerData.MagicDamage += playerData.intelligence;
    }

    public void OnChangeArmor(int amount)
    {
        playerData.armor += amount;
    }

    public void OnChangeMagicArmor(int amount)
    {
        playerData.magicArmor += amount;
    }
    public void OnChangeEvasion()
    {
        playerData.evasion += playerData.agility / 10;
    }

    public void OnChangeCritChance()
    {
        playerData.CritChance += playerData.agility / 10;
    }

    public void OnChangeCritPower()
    {
        playerData.CritPower += playerData.strength / 10;
    }

    public void OnChangeMovementSpeed(float amount)
    {
        playerData.movementSpeed += amount;
    }

    public void OnChangeCanIgnite(float amount)
    {
        playerData.canIgnite += amount;
    }
    public void OnChangeCanFreaze(float amount)
    {
        playerData.canFreaze += amount;
    }
    public void OnChangeCanShock(float amount)
    {
        playerData.canShock += amount;
    }


}
