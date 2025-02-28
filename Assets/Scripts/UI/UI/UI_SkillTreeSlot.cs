
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private UI ui;
    private Image skillImage;

    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    [SerializeField] private Color lockedSkillColor;
    public bool unlocked;


    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;


    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }
    private void Start()
    {
        skillImage = GetComponent<Image>();
        skillImage.color = lockedSkillColor;
        ui = GetComponentInParent<UI>();
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }

    public void UnlockSkillSlot()
    {
        for(int i = 0; i < shouldBeUnlocked.Length; i ++)
        {
            if(shouldBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Cannot unlock skill");
                return;
            }
        }

       /* for (int i = 0; i < shouldBeLocked.Length; i++)
        {
            Debug.Log("Cannot unlock skill");
            return;
        }
       */
        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.sKillToolTip.ShowToolTip(skillDescription,skillName);
        Vector2 mousePosition = Input.mousePosition;

        float xOffset = 0;


        if (mousePosition.x > 600)
            xOffset = -150;
        else
            xOffset = 150;

        ui.sKillToolTip.transform.position = new Vector2(mousePosition.x + xOffset, mousePosition.y + 70);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.sKillToolTip.HideToolTip();
    }
}
