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

    public void OnChangeMaxMana(int amount )
    {
        if (playerData.MaxMana > 0)
        {
            playerData.MaxMana += amount > 0 ? amount : 2 * playerData.intelligence;
            if (playerData.MaxMana < playerData.CurrentMana)
            {
                playerData.CurrentMana = playerData.MaxMana;
            }
        }
    }

    public void OnChangeMaxStamina(int amount )
    {
        if (playerData.MaxStamina > 0)
        {
            playerData.MaxStamina += amount > 0 ? amount : 3 * playerData.vitallity;
            if (playerData.MaxStamina < playerData.CurrentStamina)
            {
                playerData.CurrentStamina = playerData.MaxStamina;
            }
        }
    }

    public void OnChangeDamage(int amount)
    {
        playerData.Damage += amount > 0 ? amount : playerData.strength;
    }

    public void OnChangeMagicDamage(int amount = 0)
    {
        playerData.MagicDamage += amount > 0 ? amount : playerData.intelligence;
    }

    public void OnChangeArmor(int amount)
    {
        playerData.armor += amount;
    }

    public void OnChangeMagicArmor(int amount)
    {
        playerData.magicArmor += amount;
    }

    public void OnChangeEvasion(float amount = 0)
    {
        playerData.evasion += amount > 0 ? amount : playerData.agility / 10f;
    }

    public void OnChangeCritChance(float amount = 0)
    {
        playerData.CritChance += amount > 0 ? amount : playerData.agility / 10f;
    }

    public void OnChangeCritPower(float amount = 0)
    {
        playerData.CritPower += amount > 0 ? amount : playerData.strength / 10f;
    }

    public void OnChangeMovementSpeed(float amount)
    {
        playerData.movementSpeed += amount;
    }

    public void OnChangeCanIgnite(float amount)
    {
        playerData.canIgnite += amount;
    }

    public void OnChangeCanFreeze(float amount)
    {
        playerData.canFreaze += amount;
    }

    public void OnChangeCanShock(float amount)
    {
        playerData.canShock += amount;
    }

    public void OnChangeHpRegenRate(float amount)
    {
        playerData.hpRegenRate += amount;
    }

    public void OnChangeManaRegenRate(float amount)
    {
        playerData.manaRegenRate += amount;
    }

    public void OnChangeStaminaRegenRate(float amount)
    {
        playerData.staminaRegenRate += amount;
    }
}
