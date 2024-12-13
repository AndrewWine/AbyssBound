
using UnityEngine;



public class EnemyHealBar_UI : EntityHealthBar
{
    public GameObject enemyHealthBar;
    private void OnEnable()
    {
        enemy.isFlip += FlipUI;
    }


    private void OnDisable()
    {
        enemy.isFlip -= FlipUI;
    }

    public void FlipUI()
    {
        sliderHPTransform.Rotate(0, 180, 0);
    }

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void UpdateHealthUI()
    {
        base.UpdateHealthUI();
    }
    
    private void HideHealthBar()
    {
        enemyHealthBar.SetActive(false);
    }
    protected override void DisableSliderUI()
    {
        base.DisableSliderUI();
    }

    protected override void Update()
    {
        base.Update();
    }
}
