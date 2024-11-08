using System.Collections;
using UnityEngine;

public class Dash_Skill : Skill
{
    [Header("Dash Settings")]
    [SerializeField] private float dashCooldown; // Thời gian hồi chiêu riêng cho kỹ năng Dash
    private float dashCooldownTimer = 5;   

    protected override void Awake()
    {
        base.Awake();
        dashCooldownTimer = dashCooldown;
    }

    protected override void Update()
    {
        base.Update();
        dashCooldownTimer -= Time.deltaTime;
    }

    public override bool CanUseSkill()
    {
        
        return base.CanUseSkill() && dashCooldownTimer <= 0;
    }

    public override void ActivateSkill()
    {
        if (CanUseSkill())
        {
            cooldownTimer = cooldown;
            dashCooldownTimer = dashCooldown;
            DashSkillNotify?.Invoke();
            UseSkill();
        }
    }

    public override void UseSkill()
    {
        base.UseSkill();
   
    }

    private IEnumerator ReturnCloneToPoolAfterDelay(GameObject clone, float delay)
    {
        yield return new WaitForSeconds(delay);
    }
}
