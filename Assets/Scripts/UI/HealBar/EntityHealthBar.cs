using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract  class EntityHealthBar : MonoBehaviour
{
    public Enemy enemy;
    public Slider slider;
    public EnemyStat HP;
    public EnemyData enemyData;
    public RectTransform sliderHPTransform;
    public GameObject Healthbar;
    protected virtual void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
        HP = GetComponent<EnemyStat>();
        //this.Healthbar.SetActive(true);
    }
    protected virtual void Update()
    {
        UpdateHealthUI();
    }
    protected virtual void UpdateHealthUI()
    {
        slider.maxValue = enemyData.MaxHP;
        slider.value = HP.CurrentHP;
    }

    protected virtual void DisableSliderUI()
    {
        this.Healthbar.SetActive(false);
    }
}
