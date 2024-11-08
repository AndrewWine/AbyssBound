using System.Collections;
using System.Collections.Generic;
using UnityEngine;


using UnityEngine.UI;

public class EnemyHealBar : MonoBehaviour
{
    private Enemy enemy;
    public Slider HpBar;
    private void Awake()
    {
        enemy = GetComponent<Enemy>();
    }
}
