using System.Text;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private PlayerData playerData;
    [SerializeField] private Inventory inventory;
    [SerializeField] private CharacterStats OnStatsChanged;
    private UI ui;
    protected StringBuilder sb = new StringBuilder();

    [Header("UI Elements - Major Stats")]
    [SerializeField] private TextMeshProUGUI strengthText;
    [SerializeField] private TextMeshProUGUI agilityText;
    [SerializeField] private TextMeshProUGUI intelligenceText;
    [SerializeField] private TextMeshProUGUI vitalityText;
    [SerializeField] private TextMeshProUGUI HP;
    [SerializeField] private TextMeshProUGUI MP;
    [SerializeField] private TextMeshProUGUI STM;

    // Other fields omitted for brevity...

    [SerializeField] private string statDescription;

    private void Awake()
    {
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
        // Code for updating UI stats omitted for brevity...
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
            return "1 điểm Strength tăng 1 damage và 1% crit power.";
        else if (hoveredObject == agilityText.gameObject)
            return "1 điểm Agility tăng 1% né tránh, 1% tốc độ di chuyển và 1% crit chance.";
        else if (hoveredObject == intelligenceText.gameObject)
            return "1 điểm Intelligence tăng 1 magic damage, 3 magic resistance và 2 Max Mana.";
        else if (hoveredObject == vitalityText.gameObject)
            return "1 điểm Vitality tăng 5 HP và 3 Stamina.";
        else
            return null; // Không hiển thị tooltip cho các chỉ số khác
    }



}
