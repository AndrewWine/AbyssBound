using System.Collections;
using UnityEngine;

public class Dash_Skill : Skill
{
    
  

    protected override void Awake()
    {
        base.Awake();
      
    }

    protected override void Update()
    {
        base.Update();
       
    }

    public override bool CanUseSkill()
    {
        
        return base.CanUseSkill() ;
    }

    public override void ActivateSkill()
    {
      
        UseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
   
    }


}
