using TMPro;
using UnityEngine;

public class UI_StatTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void ShowStatToolTip(string statDescription)
    {
        description.text = statDescription; // Update description text
        gameObject.SetActive(true);         // Show the tooltip
    }

    public void HideStatTooltip()
    {
        description.text = "";              // Clear description text
        gameObject.SetActive(false);        // Hide the tooltip
    }
}
