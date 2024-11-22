﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinishTriggerEnemy : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Skeleton_Enemy>();
    private EnemyBlackBoard entity;
    private EnemyData enemyData;

    private void Awake()
    {
        entity = GetComponentInParent<EnemyBlackBoard>();
        if (entity == null)
        {
            Debug.LogError("EnemyBlackBoard không được tìm thấy trong cha của đối tượng.");
        }
        else
        {
            enemyData = entity.enemyData;
            if (enemyData == null)
            {
                Debug.LogError("EnemyData chưa được gán trong EnemyBlackBoard.");
            }
        }
    }

    private void AttackTrigger()
    {
        Debug.Log("Attack Trigger Called");

        if (entity == null || entity.attackCheck == null)
        {
            Debug.LogError("attackCheck is not assigned in EnemyBlackBoard.");
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.attackCheck.position, enemyData.attackCheckRadius);
        Debug.Log($"Số lượng collider phát hiện được: {colliders.Length}");

        foreach (var hit in colliders)
        {
            CharacterStats unitHP = hit.GetComponent<CharacterStats>();
            Player player = hit.GetComponent<Player>();
            Debug.Log($"Đối tượng: {hit.name}, Có CharacterStats: {unitHP != null}, Có Player: {player != null}");

            if (player != null && unitHP != null)
            {
                // Log evasion check
                float roll = Random.Range(0, 100);
                Debug.Log($"Evasion chance: {player.playerData.evasion}, Roll: {roll}");

                if (roll > player.playerData.evasion)
                {
                    float physicalDamage = Mathf.Max(0, enemyData.damage - player.playerData.armor);
                    float magicDamage = Mathf.Max(0, enemyData.magicDamage - player.playerData.magicArmor);

                    Debug.Log($"Physical Damage: {physicalDamage}, Magic Damage: {magicDamage}");

                    if (physicalDamage > 0) unitHP.OnCurrentHPChange(-physicalDamage);
                    if (magicDamage > 0) unitHP.OnCurrentHPChange(-magicDamage);

                    Debug.Log($"Player HP after attack: {player.playerData.CurrentHP}");
                }
                else
                {
                    Debug.Log("Attack Missed!");
                }
            }
        }
    }


    private void AnimationFinishTrigger()
    {
        if (enemy != null)
        {
            enemy.AnimationFinishTrigger();
        }
        else
        {
            Debug.LogError("Enemy không được tìm thấy.");
        }
    }

    private void OpenCounterWindow()
    {
        if (enemy != null)
        {
            enemy.OpenCounterAttackWindow();
        }
    }

    private void CloseCounterWindow()
    {
        if (enemy != null)
        {
            enemy.CloseCounterAttackWindow();
        }
    }
}
