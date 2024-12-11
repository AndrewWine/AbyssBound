using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinishTriggerEnemy : MonoBehaviour
{
    public static System.Action<int, Transform> AttackSFX;

    private Enemy enemy;
    private EntityBlackboard entityblackBoard;
    public EnemyData enemyData;

    private void Awake()
    {
        entityblackBoard = GetComponentInParent<EntityBlackboard>();
        enemy = GetComponentInParent<Enemy>();

     
    }

    private void Start()
    {
        if (entityblackBoard == null)
        {
            Debug.LogError("Không tìm thấy entityblackBoard trong GameObject cha.");
        }
        else
        {
            Debug.Log("entityblackBoard đã được tìm thấy: " + entityblackBoard.gameObject.name);
        }
    }
    private void AttackTrigger()
    {
        AttackSFX?.Invoke(2, null);

        Debug.Log("Attack Trigger Called");

        if (entityblackBoard == null || entityblackBoard.attackCheck == null)
        {
            Debug.LogError("attackCheck is not assigned in entityblackBoard.");
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(entityblackBoard.attackCheck.position, enemyData.attackCheckRadius);
   

        foreach (var hit in colliders)
        {
            CharacterStats unitHP = hit.GetComponent<CharacterStats>();
            Player player = hit.GetComponent<Player>();
            

            if (player != null && unitHP != null)
            {
                // Log evasion check
                float roll = Random.Range(0, 100);
           

                if (roll > player.playerData.evasion)
                {
                    float physicalDamage = Mathf.Max(0, enemyData.damage - player.playerData.armor);
                    float magicDamage = Mathf.Max(0, enemyData.magicDamage - player.playerData.magicArmor);

                

                    if (physicalDamage > 0) unitHP.OnCurrentHPChange(-physicalDamage);
                    if (magicDamage > 0) unitHP.OnCurrentHPChange(-magicDamage);


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
