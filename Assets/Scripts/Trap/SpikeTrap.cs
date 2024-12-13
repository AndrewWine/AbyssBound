using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Ki?m tra n?u ??i t??ng va ch?m có l?p Player
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Attack Trigger Called");

            // L?p qua t?t c? các ?i?m va ch?m trong collision
            foreach (ContactPoint2D hit in collision.contacts)
            {
                // L?y Component CharacterStats t? ??i t??ng va ch?m (Player)
                CharacterStats unitHP = hit.collider.GetComponent<CharacterStats>();
                Player player = hit.collider.GetComponent<Player>();

                // Ki?m tra n?u ??i t??ng va ch?m là Player và có CharacterStats
                if (player != null && unitHP != null)
                {
                    float physicalDamage = Mathf.Max(0f, 10f);
                    unitHP.OnCurrentHPChange(-physicalDamage);
                }
            }
        }
        else
        {
            Debug.Log("Not a player");
        }
    }
}
