using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggerofSword : MonoBehaviour
{
    private Player player;
    private PlayerBlackBoard blackBoard;
    private PlayerData playerData;
    public CircleCollider2D AttackCheck;
    public Transform swordPos;
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
            playerData = blackBoard?.playerData;
            player = playerObject.GetComponent<Player>();

            if (blackBoard == null || playerData == null || player == null)
            {
                Debug.LogError("Thiếu một hoặc nhiều thành phần trên đối tượng Player.");
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng Player.");
        }
        AttackCheck = GetComponent<CircleCollider2D>();
        swordPos = GetComponent<Transform>();
    }

    private void AttackTrigger()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(swordPos.position,playerData.attackCheckRadius);

        foreach (var hit in colliders)
        {
            EnemyStat unitHP = hit.GetComponent<EnemyStat>();
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
               unitHP.OnCurrentHPChange(player.playerData.Damage);

            }
        }
    }


}
