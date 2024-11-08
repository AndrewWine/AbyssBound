using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.EventSystems.EventTrigger;

public class UnitHP: MonoBehaviour
{
    public Action BeingHit;
    public float MaxHP = 100;
    public float CurrentHP;
   
    private void Awake()
    {
        CurrentHP = MaxHP;
    }
    public void OnCurrentHPChange(float damage)
    {
        BeingHit?.Invoke();
        CurrentHP -= damage;
    }


    public void OnChangeMaxHP(float HP)
    {
        if (MaxHP > 0)
        {
            MaxHP += HP;
            if (MaxHP < CurrentHP)
            {
                CurrentHP = MaxHP;
            }

        }
    }
}
