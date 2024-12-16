using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class UI_InGame : MonoBehaviour
{
    [SerializeField] private PlayerData playerData;
    public PlayerManager playerManager;
    [Header("Skill Image")]
    [SerializeField] private Image dashSkillImage;
    [SerializeField] private Image throwSwordImage;
    [SerializeField] private Image CloneSkillImage; 
    [SerializeField] private Image flaskImage;
    public Inventory inventory;
    [Header("Quantity")]
    [SerializeField] private TextMeshProUGUI flaskQuantity;
    [SerializeField] private TextMeshProUGUI AbyssEssenceText ;
    public bool SwordCheck = false;
    public bool DashCheck = false;
    public bool CloneCheck = false;
    public bool flaskCheck = false;

  
    private ItemData_equipment currentFlask;
    private float displayedAbyssEssence;
    [SerializeField] private float updateSpeed = 1.0f; // Tốc độ thay đổi giá trị

    public SkillsManager skillsManager ;


    private void OnEnable()
    {
        Inventory.UsedFlask += SetCooldownOfFlask;
        playerManager.NotifyUpdateCurrency += UpdateAbyssEssence;
        SwordSkill_Controller.CheckSword += SetCoolDownOfThrowSword;
        skillsManager.CloneSkillCoolDown += SetCoolDownOfClone;
        skillsManager.DashSkillCoolDown += SetCoolDownOfDashSkill;
    }

    private void OnDisable()
    {
        Inventory.UsedFlask -= SetCooldownOfFlask;
        playerManager.NotifyUpdateCurrency -= UpdateAbyssEssence;
        skillsManager.CloneSkillCoolDown -= SetCoolDownOfClone;
        skillsManager.DashSkillCoolDown -= SetCoolDownOfDashSkill;
        SwordSkill_Controller.CheckSword -= SetCoolDownOfThrowSword;

    }

    private void Awake()
    {
        UpdateAbyssEssence();
    }

    private void Start()
    {
     
    }


    private void Update()
    {
        UpdateAbyssEssence();
        if (SwordCheck == true)
        {
            CheckCooldownOf(throwSwordImage, 10);
            //SwordCheck = false;
        }

        if (DashCheck == true)
        {
            CheckCooldownOf(dashSkillImage, 3);
            //DashCheck = false;
        }
        if (CloneCheck == true)
        {
            CheckCooldownOf(CloneSkillImage, 2);
            //CloneCheck = false;
        }

        if (flaskCheck == true)
        {
            CheckCooldownOf(flaskImage, 5);
        }

        // Pass the currentFlask instance
        CheckQuantityOfFlask();
    }


    private void CheckQuantityOfFlask()
    {

        // Cập nhật số lượng bình
        flaskQuantity.text = inventory.flaskQuantity.ToString();
    }




    private void UpdateAbyssEssence()
    {
        // Nội suy giá trị hiển thị dần dần đến giá trị thực tế
        displayedAbyssEssence = Mathf.Lerp(displayedAbyssEssence, playerData.AbyssEssence, Time.deltaTime * updateSpeed);

        // Hiển thị giá trị đã nội suy
        AbyssEssenceText.text = Mathf.RoundToInt(displayedAbyssEssence).ToString();
    }

    public void SetCoolDownOfDashSkill()
    {
        SetCoolDownOf(dashSkillImage);
        DashCheck = true;
    }

    public void SetCoolDownOfClone()
    {
        SetCoolDownOf(CloneSkillImage);
        CloneCheck = true;
    }

    public void SetCoolDownOfThrowSword(bool check)
    {
       if(!check)
        {
            SetCoolDownOf(throwSwordImage);
            SwordCheck = true;
        }
       else
        {
            CheckCooldownOf(throwSwordImage, 0);
        }

    }

    public void SetCooldownOfFlask()
    {
        SetCoolDownOf(flaskImage);
        flaskCheck = true;

    }
    private void SetCoolDownOf(Image _image)
    {
        if( _image.fillAmount <= 0)
        {
            _image.fillAmount = 1;
        }
    }

    private void CheckCooldownOf(Image _image, float _cooldown)
    {
        if(_image.fillAmount > 0)
        {
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
        }
    }
}
