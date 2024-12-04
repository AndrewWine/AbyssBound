using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimationTriggersDamageOfPlayer : MonoBehaviour
{
    public System.Action<int,Transform> PlayerSFXAtk;
    private Player player;
    private PlayerBlackBoard blackBoard;

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            blackBoard = playerObject.GetComponent<PlayerBlackBoard>();
            player = playerObject.GetComponent<Player>();
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng Player.");
        }

    }

    private void AttackTrigger()
    {
        if (blackBoard == null || blackBoard.attackCheck == null)
        {
            Debug.LogError("PlayerBlackBoard hoặc attackCheck chưa được thiết lập.");
            return;
        }

        // Tìm tất cả các đối tượng trong phạm vi tấn công
        Collider2D[] colliders = Physics2D.OverlapCircleAll(blackBoard.attackCheck.position, player.playerData.attackCheckRadius);

        foreach (var hit in colliders)
        {
            // Lấy các thành phần liên quan trên đối tượng bị va chạm
            Enemy enemy = hit.GetComponent<Enemy>();
            EnemyStat unitHP = hit.GetComponent<EnemyStat>();

            if (enemy != null && unitHP != null)
            {
                // Tính toán sát thương (Crit hoặc không)
                float damage = player.playerData.Damage;
                if (Random.Range(0, 100) < player.playerData.CritChance)
                {
                    damage += damage * player.playerData.CritPower;
                    Debug.Log("Crit Hit! Damage: " + damage);
                    unitHP.OnCurrentHPChange(damage - unitHP.enemyData.Armor);
                    enemy.TakeDamage();
                    PlayerSFXAtk?.Invoke(0, null);

                }

                else
                {
                    // Áp dụng sát thương
                    unitHP.OnCurrentHPChange(damage);
                    enemy.TakeDamage();
                    PlayerSFXAtk?.Invoke(0, null);
                }

                Debug.Log($"Applied {damage} damage to {enemy.name}.");
            }
        }
    }




}