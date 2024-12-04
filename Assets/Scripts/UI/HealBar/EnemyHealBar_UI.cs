using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealBar_UI : MonoBehaviour
{
    private Enemy enemy;
    private Slider slider;
    private EnemyStat HP;
    public EnemyData enemyData;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        slider = GetComponentInChildren<Slider>();
        HP = GetComponent<EnemyStat>();
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = enemyData.MaxHP;
        slider.value = HP.CurrentHP;
    }


}
