using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationFinishTriggerEnemy : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Skeleton_Enemy>();
    private void AnimationFinishTrigger()
    {
        enemy.AnimationFinishTrigger();
    }
}
