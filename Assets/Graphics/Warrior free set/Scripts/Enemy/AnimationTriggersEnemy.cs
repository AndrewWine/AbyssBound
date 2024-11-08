using System.Collections;
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
            Debug.LogError("attackCheck không được gán trong EnemyBlackBoard.");
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(entity.attackCheck.position, enemyData.attackCheckRadius);

        foreach (var hit in colliders)
        {
            UnitHP unitHP = hit.GetComponent<UnitHP>();
            Player player = hit.GetComponent<Player>();

            if (player != null && unitHP != null )
            { 
                if(Random.Range(0, 100) > player.playerData.evasion)
                {
                   
                    if (enemyData.damage > 0) unitHP.OnCurrentHPChange(enemyData.damage - player.playerData.armor);
                    if (enemyData.magicDamage > 0) unitHP.OnCurrentHPChange(enemyData.damage - player.playerData.magicArmor);
                }
                else
                {
                    Debug.Log("Attack Miss");
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
