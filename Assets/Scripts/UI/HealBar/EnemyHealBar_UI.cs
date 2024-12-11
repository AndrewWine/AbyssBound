using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class EnemyHealBar_UI : MonoBehaviour
{
    private Enemy enemy;
    private Slider slider;
    private EnemyStat HP;
    public EnemyData enemyData;
    public RectTransform sliderHPTransform;
    public GameObject Healthbar;
    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        slider = GetComponentInChildren<Slider>();
        HP = GetComponent<EnemyStat>();
        this.Healthbar.SetActive(true);
    }

    private void OnEnable()
    {
        enemy.isFlip += FlipUI;
    }


    private void OnDisable()
    {
        enemy.isFlip -= FlipUI;
    }

    public void DisableSliderUI()
    {
        this.Healthbar.SetActive(false);
    }
    private void Update()
    {
        UpdateHealthUI();
    }

    public void FlipUI()
    {
        sliderHPTransform.Rotate(0, 180, 0);
      
    }
    private void UpdateHealthUI()
    {
        slider.maxValue = enemyData.MaxHP;
        slider.value = HP.CurrentHP;
    }

   
}
