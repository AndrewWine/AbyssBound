using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealBar_UI : MonoBehaviour
{
    private Enemy enemy;
    private Slider slider;
    private UnitHP HP;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        slider = GetComponentInChildren<Slider>();
        HP = GetComponent<UnitHP>();
    }

    private void Update()
    {
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = HP.MaxHP;
        slider.value = HP.CurrentHP;
    }


}
