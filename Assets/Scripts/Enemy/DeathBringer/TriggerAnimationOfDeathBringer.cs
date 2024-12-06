using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimationOfDeathBringer : AnimationFinishTriggerEnemy
{
   private Enemy_DeathBringer enemy_DeathBringer => GetComponentInParent<Enemy_DeathBringer>();
    private void MakeInvisible() => enemy_DeathBringer.fx.MakeTransperent(true);
    private void Makevisible() => enemy_DeathBringer.fx.MakeTransperent(false);

}
