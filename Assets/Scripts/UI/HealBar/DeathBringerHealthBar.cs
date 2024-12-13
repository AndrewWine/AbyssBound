using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathBringerHealthBar : EntityHealthBar
{
    protected override void Awake()
    {
        InactiveHealthBar();
    }

    
    public void ActiveHealthBar()
    {
        Healthbar.SetActive(true);
        Debug.Log("Active healthbar");
    }

    public void InactiveHealthBar()
    {
        Healthbar.SetActive(false);
    }

    protected override void UpdateHealthUI()
    {
        base.UpdateHealthUI();
    }
}
