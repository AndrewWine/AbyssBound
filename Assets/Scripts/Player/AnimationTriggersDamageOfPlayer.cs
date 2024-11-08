using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTriggersDamageOfPlayer : MonoBehaviour
{
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

        Collider2D[] colliders = Physics2D.OverlapCircleAll(blackBoard.attackCheck.position, player.playerData.attackCheckRadius);

        foreach (var hit in colliders)
        {
            Enemy _target = hit.GetComponent<Enemy>();
            UnitHP unitHP = hit.GetComponent<UnitHP>();
            if (_target != null)
            {
                if(player.playerData.CritChance > Random.Range(0,100))
                {
                    unitHP.OnCurrentHPChange(player.playerData.Damage + player.playerData.Damage * player.playerData.CritPower);
                    Debug.Log("Crit hit");
                    Debug.Log(player.playerData.Damage + player.playerData.Damage * player.playerData.CritPower);
                    Debug.Log($"Damage: {player.playerData.Damage}, CritPower: {player.playerData.CritPower}");
                    Debug.Log($"Total Damage: {player.playerData.Damage + player.playerData.Damage * player.playerData.CritPower}");

                }
                else
                unitHP.OnCurrentHPChange(player.playerData.Damage);
            }
        }
    }



}
