using System;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public PlayerData playerData;
    public PlayerInputHandler inputHandler;
    public SkillsManager skillsManager;
    public event Action OnStatsChanged;
    public Player player;
    public static System.Action<int, Transform> playerbeinghit;
    private void Awake()
    {
        SaveManager.resetPlayerData += ResetDefaultStats;
        playerData.CurrentMana = playerData.MaxMana;
        playerData.CurrentStamina = playerData.MaxStamina;
        inputHandler.UseStamina += OnCurrentStaminaChange;
        inputHandler.UseMana += OnCurrentManaChange;
        skillsManager.UseMana += OnCurrentManaChange;
        skillsManager.UseStamina += OnCurrentStaminaChange;
    }

    private void OnDisable()
    {
        SaveManager.resetPlayerData -= ResetDefaultStats;
        inputHandler.UseStamina -= OnCurrentStaminaChange;
        inputHandler.UseMana -= OnCurrentManaChange;
        skillsManager.UseMana -= OnCurrentManaChange;
        skillsManager.UseStamina -= OnCurrentStaminaChange;
    }

    private void NotifyStatChange()
    {
        OnStatsChanged?.Invoke(); // Kích hoạt sự kiện khi có thay đổi chỉ số
    }

    public void OnchangeSTRENGTH(int amount)
    {
        playerData.strength += amount;
        NotifyStatChange();
    }


    public void OnchangeAGILITY(int amount)
    {
        playerData.agility += amount;
        NotifyStatChange();
    }

    public void OnchangeINTELLIGENCE(int amount)
    {
        playerData.intelligence += amount;
        NotifyStatChange();
    }

    public void OnchangeVITALITY(int amount)
    {
        playerData.vitallity += amount;
        NotifyStatChange();
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

    public void OnCurrentHPChange(float amount)
    {
        if(amount < 0)
        {
            playerbeinghit?.Invoke(33, null);
            playerData.CurrentHP += amount;
            player.TakeDamage();
        }
       
        else if (amount > 0) 
        {
            playerData.CurrentHP += amount;
        }
        if (playerData.CurrentHP > playerData.MaxHP)
        {

            playerData.CurrentHP = playerData.MaxHP;
        }
     
    }

    public void OnChangeMaxHP(int amount)
    {
        if (playerData.MaxHP > amount)
        {
            playerData.MaxHP += amount * 5;
            if (playerData.MaxHP < playerData.CurrentHP)
            {
                playerData.CurrentHP = playerData.MaxHP;
            }
        }
        NotifyStatChange();
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

    public void ResetDefaultStats()
    {
        // Reset các chỉ số cơ bản
        playerData.MaxHP = 100;
        playerData.CurrentHP = playerData.MaxHP;

        playerData.MaxMana = 30;
        playerData.CurrentMana = playerData.MaxMana;

        playerData.MaxStamina = 50;
        playerData.CurrentStamina = playerData.MaxStamina;

        // Reset chỉ số thuộc tính chính
        playerData.strength = 1;
        playerData.agility = 1;
        playerData.intelligence = 1;
        playerData.vitallity = 1;

        // Reset sát thương và giáp
        playerData.Damage = 10;
        playerData.MagicDamage = 5;
        playerData.armor = 1;
        playerData.magicArmor = 0;

        // Reset tỷ lệ né tránh, chí mạng
        playerData.evasion = 5f;
        playerData.CritChance = 1f; // Tỷ lệ chí mạng 1%
        playerData.CritPower = 1.5f; // Sát thương chí mạng x1.5

        // Reset tốc độ di chuyển
        playerData.movementSpeed = 5f;

        // Reset hiệu ứng đặc biệt
        playerData.canIgnite = 0f;
        playerData.canFreaze = 0f;
        playerData.canShock = 0f;

        // Reset tốc độ hồi phục
        playerData.hpRegenRate = 1f; // Hồi 1 HP mỗi giây
        playerData.manaRegenRate = 0.5f; // Hồi 0.5 mana mỗi giây
        playerData.staminaRegenRate = 1f; // Hồi 1 stamina mỗi giây

        // Gọi sự kiện thay đổi chỉ số (nếu có)
        NotifyStatChange();
    }
}
