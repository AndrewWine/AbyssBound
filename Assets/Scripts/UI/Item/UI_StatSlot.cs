
using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Inventory inventory;
    [SerializeField] private CharacterStats OnStatsChanged;

    [Header("UI Elements - Major Stats")]
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI intelligenceText;
    [SerializeField] private TextMeshProUGUI vitalityText;
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI MP;
    [SerializeField] private TextMeshProUGUI STM;


    [Header("UI Elements - Offensive Stats")]
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI magicDamageText;
    [SerializeField] private TextMeshProUGUI critChanceText;
    [SerializeField] private TextMeshProUGUI critPowerText;

    [Header("UI Elements - Defensive Stats")]
    [SerializeField] private TextMeshProUGUI armorText;
    [SerializeField] private TextMeshProUGUI magicArmorText;
    [SerializeField] private TextMeshProUGUI evasionText;

    [Header("UI Elements - Other Stats")]
    [SerializeField] private TextMeshProUGUI movementSpeedText;

    [Header("UI Elements - Buffs")]
    [SerializeField] private TextMeshProUGUI staminaRegenRateText;
    [SerializeField] private TextMeshProUGUI hpRegenRateText;
    [SerializeField] private TextMeshProUGUI manaRegenRateText;

    [Header("UI Elements - Effects")]
    [SerializeField] private TextMeshProUGUI lifestealText;
    [SerializeField] private TextMeshProUGUI canIgniteText;
    [SerializeField] private TextMeshProUGUI canFreazeText;
    [SerializeField] private TextMeshProUGUI canShockText;

    private void Awake()
    {
        OnValidate();
    }


    private void OnEnable()
    {
        inventory.UpdateStats += OnValidate;
        OnStatsChanged.OnStatsChanged += OnValidate;
    }

    private void OnDisable()
    {
        inventory.UpdateStats -= OnValidate;
        OnStatsChanged.OnStatsChanged -= OnValidate;
    }


    private void OnValidate()
    {
        // Major Stats
        strengthText.text = "Strengh: "+ playerData.strength.ToString();
        agilityText.text = "Agility: " + playerData.agility.ToString();
        intelligenceText.text = "Intelligence: " + playerData.intelligence.ToString();
        vitalityText.text = "Vitality: " + playerData.vitallity.ToString();
        HP.text = "HP: " + playerData.MaxHP.ToString();
        MP.text = "MP: " + playerData.MaxMana.ToString();
        STM.text = "STM: " + playerData.MaxStamina.ToString(); 
        // Offensive Stats
        damageText.text = "Physic Damage: " + playerData.Damage.ToString();
        magicDamageText.text = "Magic Damage: " +playerData.MagicDamage.ToString();
        critChanceText.text = "Crit Chance: " + playerData.CritChance.ToString("F2") + "%";
        critPowerText.text = "Crit Power: " + playerData.CritPower.ToString("F2") + "%";

        // Defensive Stats
        armorText.text = "Armor: " + playerData.armor.ToString();
        magicArmorText.text = "Magic Armor: " + playerData.magicArmor.ToString();
        evasionText.text = "Evasion: " + playerData.evasion.ToString("F2") + "%";

        // Other Stats
        movementSpeedText.text = "Move Speed: " + playerData.movementSpeed.ToString("F2");

        // Buffs
        staminaRegenRateText.text = "STM regen: " + playerData.staminaRegenRate.ToString("F2") + "/s";
        hpRegenRateText.text = "HP regen: " + playerData.hpRegenRate.ToString("F2") + "/s";
        manaRegenRateText.text = "MP regen: " + playerData.manaRegenRate.ToString("F2") + "/s";

        // Effects
        lifestealText.text = "Life Steal: " + playerData.lifesteal.ToString("F2") + "%";
        canIgniteText.text = "Ignite: " + playerData.canIgnite.ToString("F2") + "%";
        canFreazeText.text = "Freaze: " + playerData.canFreaze.ToString("F2") + "%";
        canShockText.text = "Shock: " + playerData.canShock.ToString("F2") + "%";

    }
}
