using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;

public class Skill : MonoBehaviour
{


   [Header("Observer Notify")]
    public static Action DashSkillNotify; // Event to notify when the skill can be used



    [Header("Cooldown Variable")]
    [SerializeField] protected float cooldown = 3; // Cooldown time
    [SerializeField] protected float cooldownTimer = 3; // Timer for cooldown

    [Header("Component")]
    protected PlayerBlackBoard blackBoard;
    public SkillsManager skillsManager;

    protected virtual void Start()
    {

    }
    protected virtual void Awake()
    {
        blackBoard = GetComponent<PlayerBlackBoard>();

    }

    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime; // Reduce cooldown timer
        CanUseSkill();
    }

    public virtual bool CanUseSkill()
    {
        return cooldownTimer <= 0; // Check if the skill can be used
    }

    public virtual void ActivateSkill()
    {
        if (CanUseSkill()) // If the skill can be used
        {
           // Reset cooldown timer
            DashSkillNotify?.Invoke(); // Notify subscribers that the skill is ready to use
            UseSkill(); // Use the skill
        }
    }

    public virtual void UseSkill()
    {
        // Perform the action for the skill (e.g., dash)
        Debug.Log("Using skill");
        cooldownTimer = cooldown;
    }

   
}
