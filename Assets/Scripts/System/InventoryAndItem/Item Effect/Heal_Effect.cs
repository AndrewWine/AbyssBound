using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Heal effect", menuName = "Data/Item effect/Heal effect")]

public class Heal_Effect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healPercent;

    public override void ExecuteEffect(Transform _enemyPosition)
    {
        CharacterStats characterStats = GameObject.Find("Player").GetComponent<CharacterStats>();
        int healAmount = Mathf.RoundToInt(100f * healPercent);
        characterStats.OnCurrentHPChange(healAmount);
    }
}
