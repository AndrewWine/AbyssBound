using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Pool;
using Unity.VisualScripting;

public class Skill : MonoBehaviour
{


   [Header("Observer Notify")]
    public static Action DashSkillNotify; // Event to notify when the skill can be used



    [Header("Cooldown Variable")]
    [SerializeField] public float cooldown; // Cooldown time
    [SerializeField] protected float cooldownTimer = 0; // Timer for cooldown starts at 0

    [Header("Component")]
    protected PlayerBlackBoard blackBoard;
    public SkillsManager skillsManager;

    protected virtual void Start()
    {
        cooldown = 3;
        cooldownTimer = 0;
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
