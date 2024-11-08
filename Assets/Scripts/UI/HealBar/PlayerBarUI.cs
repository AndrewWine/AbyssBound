using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarUI : MonoBehaviour
{
    public UnitHP HP;
    public PlayerData playerData;

    [SerializeField] private Transform hpBar;
    [SerializeField] private Transform mpBar;
    [SerializeField] private Transform stmBar;

    private Slider HPslider;
    private Slider MPslider;
    private Slider STMslider;

    private float smoothSpeed = 10f; // Tốc độ làm mượt cho tất cả thanh
    private float displayedHP;       // Giá trị HP hiển thị mượt mà
    private float displayedMP;       // Giá trị MP hiển thị mượt mà
    private float displayedStamina;  // Giá trị Stamina hiển thị mượt mà

    private void Awake()
    {
        // Lấy slider từ mỗi thanh
        HPslider = hpBar.GetComponentInChildren<Slider>();
        MPslider = mpBar.GetComponentInChildren<Slider>();
        STMslider = stmBar.GetComponentInChildren<Slider>();

        // Khởi tạo giá trị hiển thị bằng giá trị thực tế
        displayedHP = HP.CurrentHP;
        displayedMP = playerData.CurrentMana;
        displayedStamina = playerData.CurrentStamina;
    }

    private void Update()
    {
        UpdatePlayerBarUI();
    }

    private void UpdatePlayerBarUI()
    {
        // Làm mượt giá trị HP
        displayedHP = Mathf.Lerp(displayedHP, HP.CurrentHP, smoothSpeed * Time.deltaTime);
        HPslider.maxValue = HP.MaxHP;
        HPslider.value = displayedHP;

        // Làm mượt giá trị MP
        displayedMP = Mathf.Lerp(displayedMP, playerData.CurrentMana, smoothSpeed * Time.deltaTime);
        MPslider.maxValue = playerData.MaxMana;
        MPslider.value = displayedMP;

        // Làm mượt giá trị Stamina
        displayedStamina = Mathf.Lerp(displayedStamina, playerData.CurrentStamina, smoothSpeed * Time.deltaTime);
        STMslider.maxValue = playerData.MaxStamina;
        STMslider.value = displayedStamina;

       
    }
}
