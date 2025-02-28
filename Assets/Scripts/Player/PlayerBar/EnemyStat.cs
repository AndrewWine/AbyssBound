﻿using UnityEngine;
using System;
using TMPro;
public class EnemyStat : MonoBehaviour
{
    public static Action<float> DropAbyssEssence;

    public EnemyData enemyData;
    public Enemy enemy;

    [SerializeField] private TextMeshProUGUI levelText;

    [Header("Enemy stats")]
    public float CurrentHP;
    public float RatePowerLevel;
    private float baseMaxHP = 150;
    private float baseDamage = 25;
    private float baseArmor = 5;
    private float baseMagicArmor = 3;
    public float baseAbyssEssenceDropAmount = 20;
    public float BossPower;
    private void OnEnable()
    {
        enemy.isDeath += CheckisDeath;
    }
    private void OnDisable()
    {
        enemy.isDeath -= CheckisDeath;

    }
    private void Awake()
    {
        levelText.text = "Level: " + enemyData.Level;
        RatePowerLevel = enemyData.Level / 10f;
        BossPower = enemyData.Level / 5f;
        if (!enemy.isBoss)
        {
            enemyData.Level = UnityEngine.Random.Range(1, 5);
        }
    }

    private void Start()
    {
        ModifyStatBaseOnLV();
        // Kiểm tra giá trị khởi tạo
        Debug.Log($"Initial MaxHP: {enemyData.MaxHP}, CurrentHP: {CurrentHP}");
    }

    public void OnCurrentHPChange(float damage)
    {
        CurrentHP -= damage;
    }

    public void ModifyStatBaseOnLV()
    {
        if (enemyData.Level >= 1 && !enemy.isBoss)
        {
            // Kiểm tra giá trị trung gian
            Debug.Log($"Level: {enemyData.Level}, RatePowerLevel: {RatePowerLevel}");

            // Cập nhật các giá trị dựa trên giá trị gốc
            enemyData.MaxHP = baseMaxHP + (baseMaxHP * enemyData.Level * RatePowerLevel);
            enemyData.damage = baseDamage + (baseDamage * enemyData.Level * RatePowerLevel);
            enemyData.Armor = baseArmor + (baseArmor * enemyData.Level * RatePowerLevel) / 10f;
            enemyData.MagicArmor = baseMagicArmor + (baseMagicArmor * enemyData.Level * RatePowerLevel) / 15f;
            enemyData.AbyssEssenceDropAmount = baseAbyssEssenceDropAmount * (RatePowerLevel + 1);
            CurrentHP = enemyData.MaxHP;
            // Kiểm tra giá trị sau tính toán
            Debug.Log($"Updated MaxHP: {enemyData.MaxHP}");
        }
        else if (enemyData.Level >= 1 && enemy.isBoss) 
        {
            // Cập nhật các giá trị dựa trên giá trị gốc
            enemyData.MaxHP = baseMaxHP + (baseMaxHP * enemyData.Level * BossPower);
            enemyData.damage = baseDamage + (enemyData.Level * BossPower);
            enemyData.magicDamage = baseDamage + (enemyData.Level * BossPower);
            enemyData.Armor = baseArmor + (baseArmor * enemyData.Level * BossPower) / 10f;
            enemyData.MagicArmor = baseMagicArmor + (baseMagicArmor * enemyData.Level * BossPower) / 15f;
            enemyData.AbyssEssenceDropAmount = baseAbyssEssenceDropAmount * (BossPower + 10);
            CurrentHP = enemyData.MaxHP;
        }

       
    }
    
    public void CheckisDeath()
    {
        DropAbyssEssence?.Invoke(enemyData.AbyssEssenceDropAmount);
    }
}
