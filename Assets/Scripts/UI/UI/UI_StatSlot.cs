using System.Text;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    protected StringBuilder sb = new StringBuilder();
    public CharacterStats character;
    public PlayerManager playerManager;
    private UI ui;

    [Header("variable")]
    public float growthRate = 0.2f; // Hệ số tăng trưởng, bạn có thể điều chỉnh giá trị này
    public float STRPointUsed;
    public float AGLPointUsed;
    public float INTPointUsed;
    public float VITPointUsed;

    [SerializeField] private string statDescription;

    [Header("Button Increase Stat points")]
    [SerializeField] private Button strengthButton;
    [SerializeField] private Button agilitythButton;
    [SerializeField] private Button intelligenceButton;
    [SerializeField] private Button vitalityButton;



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
        SetPointRequireToUpgradeStat();
        CheckButtonPlusStatsPress(); // Đăng ký sự kiện một lần tại đây
        ui = GetComponentInParent<UI>();
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
        strengthText.text = "Strengh: " + playerData.strength.ToString();
        agilityText.text = "Agility: " + playerData.agility.ToString();
        intelligenceText.text = "Intelligence: " + playerData.intelligence.ToString();
        vitalityText.text = "Vitality: " + playerData.vitallity.ToString();
        HP.text = "HP: " + playerData.CurrentHP.ToString();
        MP.text = "MP: " + playerData.CurrentMana.ToString();
        STM.text = "STM: " + playerData.CurrentStamina.ToString();
        // Offensive Stats
        damageText.text = "Physic Damage: " + playerData.Damage.ToString();
        magicDamageText.text = "Magic Damage: " + playerData.MagicDamage.ToString();
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
    public string GetDescriptionStat()
    {
        sb.Length = 0; // Clear previous content in StringBuilder
        sb.Append(statDescription); // Append the description
        return sb.ToString(); // Return the final description string
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        string description = GetHoveredStatDescription(eventData.pointerEnter);
        if (!string.IsNullOrEmpty(description)) // Chỉ hiển thị nếu description không null
        {
            ui.statTooltip.ShowStatToolTip(description);
        }
     
    }




    public void OnPointerExit(PointerEventData eventData)
    {
        ui.statTooltip.HideStatTooltip();
    }

    private string GetHoveredStatDescription(GameObject hoveredObject)
    {
        if (hoveredObject == strengthText.gameObject)
            return "1 điểm Strength tăng 1 damage và 1% crit power." + "Need " + STRPointUsed + " to upgrade this stat";
        else if (hoveredObject == agilityText.gameObject)
            return "1 điểm Agility tăng 1% né tránh, 1% tốc độ di chuyển và 1% crit chance." + "Need " + AGLPointUsed + " to upgrade this stat";
        else if (hoveredObject == intelligenceText.gameObject)
            return "1 điểm Intelligence tăng 1 magic damage, 3 magic resistance và 2 Max Mana." + "Need " + INTPointUsed + " to upgrade this stat";
        else if (hoveredObject == vitalityText.gameObject)
            return "1 điểm Vitality tăng 5 HP và 3 Stamina." + "Need " + VITPointUsed + " to upgrade this stat";
        else
            return null; // Không hiển thị tooltip cho các chỉ số khác
    }

  


    //Cộng chỉ số bằng nút bấm
    public void CheckButtonPlusStatsPress()
    {
        strengthButton.onClick.AddListener(() => {
            if (STRPointUsed <= playerData.AbyssEssence) 
            {
                character.OnchangeSTRENGTH(1);
                playerManager.OnCurrencyChange(-STRPointUsed);
                SetPointRequireToUpgradeStat();
                OnValidate();

            }
        });

        agilitythButton.onClick.AddListener(() => {
            if (AGLPointUsed <= playerData.AbyssEssence)
            {
                character.OnchangeAGILITY(1);
                playerManager.OnCurrencyChange(-AGLPointUsed);
                SetPointRequireToUpgradeStat();
                OnValidate();

            }
        });

        intelligenceButton.onClick.AddListener(() => {
            if (INTPointUsed <= playerData.AbyssEssence)
            {
                character.OnchangeINTELLIGENCE(1);
                playerManager.OnCurrencyChange(-INTPointUsed);
                SetPointRequireToUpgradeStat();
                OnValidate();

            }
        });

        vitalityButton.onClick.AddListener(() => {
            if (VITPointUsed <= playerData.AbyssEssence)
            {
                character.OnchangeVITALITY(1);
                playerManager.OnCurrencyChange(-VITPointUsed);
                SetPointRequireToUpgradeStat();
                OnValidate();

            }
        });


    }

    public void SetPointRequireToUpgradeStat()
    {
        STRPointUsed = Mathf.RoundToInt(10 * playerData.strength * (1 + growthRate * playerData.strength));
        AGLPointUsed = Mathf.RoundToInt(10 * playerData.agility * (1 + growthRate * playerData.agility));
        INTPointUsed = Mathf.RoundToInt(10 * playerData.intelligence * (1 + growthRate * playerData.intelligence));
        VITPointUsed = Mathf.RoundToInt(10 * playerData.vitallity * (1 + growthRate * playerData.vitallity));
    }
}

