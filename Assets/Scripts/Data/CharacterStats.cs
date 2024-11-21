using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerInputHandler inputHandler;
    public event Action OnStatsChanged;

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

    private void NotifyStatChange()
    {
        OnStatsChanged?.Invoke(); // Kích hoạt sự kiện khi có thay đổi chỉ số
    }

    public void OnCurrentManaChange(int amount)
    {
        playerData.CurrentMana += amount;
 
    }

    public void OnCurrentStaminaChange(int amount)
    {
        if (playerData.CurrentStamina > amount)
            playerData.CurrentStamina += amount;
    }

    public void OnChangeMaxMana(int amount)
    {
        if (playerData.MaxMana > amount)
        {
            playerData.MaxMana += amount *  2 ;
            if (playerData.MaxMana < playerData.CurrentMana)
            {
                playerData.CurrentMana = playerData.MaxMana;
            }
        }
        NotifyStatChange();
    }

    public void OnChangeMaxStamina(int amount)
    {
        if (playerData.MaxStamina > 0)
        {
            playerData.MaxStamina += amount > 0 ? amount : 3 * playerData.vitallity;
            if (playerData.MaxStamina < playerData.CurrentStamina)
            {
                playerData.CurrentStamina = playerData.MaxStamina;
            }
        }
        NotifyStatChange();
    }

    public void OnChangeDamage(int amount)
    {
        playerData.Damage += amount;
        NotifyStatChange();
    }

    public void OnChangeMagicDamage(int amount = 0)
    {
        playerData.MagicDamage += amount > 0 ? amount : playerData.intelligence;
        NotifyStatChange();
    }

    public void OnChangeArmor(int amount)
    {
        playerData.armor += amount;
        NotifyStatChange();
    }

    public void OnChangeMagicArmor(int amount)
    {
        playerData.magicArmor += amount;
        NotifyStatChange();
    }

    public void OnChangeEvasion(float amount = 0)
    {
        playerData.evasion += amount > 0 ? amount : playerData.agility / 10f;
        NotifyStatChange();
    }

    public void OnChangeCritChance(float amount = 0)
    {
        playerData.CritChance += amount > 0 ? amount : playerData.agility / 10f;
        NotifyStatChange();
    }

    public void OnChangeCritPower(float amount = 0)
    {
        playerData.CritPower += amount > 0 ? amount : playerData.strength / 10f;
        NotifyStatChange();
    }

    public void OnChangeMovementSpeed(float amount)
    {
        playerData.movementSpeed += amount;
        NotifyStatChange();
    }

    public void OnChangeCanIgnite(float amount)
    {
        playerData.canIgnite += amount;
        NotifyStatChange();
    }

    public void OnChangeCanFreeze(float amount)
    {
        playerData.canFreaze += amount;
        NotifyStatChange();
    }

    public void OnChangeCanShock(float amount)
    {
        playerData.canShock += amount;
        NotifyStatChange();
    }

    public void OnChangeHpRegenRate(float amount)
    {
        playerData.hpRegenRate += amount;
        NotifyStatChange();
    }

    public void OnChangeManaRegenRate(float amount)
    {
        playerData.manaRegenRate += amount;
        NotifyStatChange();
    }

    public void OnChangeStaminaRegenRate(float amount)
    {
        playerData.staminaRegenRate += amount;
        NotifyStatChange();
    }
}
