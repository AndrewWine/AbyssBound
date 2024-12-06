using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBringerSpellController : MonoBehaviour
{
    [SerializeField] private Transform check;
    [SerializeField] private Vector2 boxSize;
    [SerializeField] private LayerMask whatisPlayer;
    public EnemyData enemyData;

    private void Awake()
    {
       
    }

    private void SetupSpell(EnemyData _stat) => enemyData = _stat;
    private void AnimationTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(check.position, boxSize ,whatisPlayer);


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

    private void OnDrawGizmos() =>Gizmos.DrawWireCube(check.position, boxSize);
    
    private void SelfDestroy() => Destroy(gameObject);
}
