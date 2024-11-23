using Unity.VisualScripting;
using UnityEngine;
using System;
public class PlayerInputHandler : MonoBehaviour
{
    public PlayerData playerData; // Reference to player data scriptable object
    public Player player;
    public SkillsManager skillManager;
    public PlayerStateMachine stateMachine;

    public Action<int> UseMana;
    public Action<int> UseStamina;

    // Input variables
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput;
    public bool DashInputStop { get; private set; }
    public bool SlideInput { get; private set; }
    public bool LeftClick {  get; set; }
    public bool RightClick { get; set; }
    public bool PressedKeyQ;
    private bool isDead = false; // Thêm biến isDead

    // Input hold times
    [SerializeField] private float inputHoldTime = 0.2f; // How long to hold the input
    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float slideInputStartTime;
    private float leftclickInputStartTime = 0;
    private float righttclickInputStartTime = 0;

    private void Awake()
    {
        player.isDeath += SetIsDead;
    }

    private void OnDisable()
    {
        player.isDeath -= SetIsDead;
    }
    void Update()
    {
        if (isDead) return; // Nếu isDead là true, không nhận input nữa
        // Check hold times for inputs
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckSlideInputHoldTime();
        OldInputFunction();
        JumpInputFunction();
        DashInputFunction();
        SlideInputFunction();
        LeftClickFunction();
        RightClickFunction();
        AimSword();
        ClonePlayer();

        playerData.UsageTimer += Time.deltaTime;
    }
    public void ClonePlayer()
    {
        if (Input.GetKeyDown
            (KeyCode.E) && playerData.CurrentMana >= 5)
        {
            skillManager.ActivateDashCloneAttack();
            UseMana?.Invoke(-5);
        }
    }
    public void AimSword()
    {
        if(Input.GetKeyDown(KeyCode.Q) && playerData.CurrentStamina >= 10)
        {
            PressedKeyQ = true;
            UseStamina?.Invoke(-10);

        }
        else if(Input.GetKeyUp(KeyCode.Q))
        {
            PressedKeyQ = false;
        }
    }
    public void OldInputFunction()
    {
        // Capture raw movement input from the horizontal and vertical axes
        RawMovementInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Normalize input values to ensure they're either -1, 0, or 1
        NormInputX = Mathf.RoundToInt(RawMovementInput.x);
        NormInputY = Mathf.RoundToInt(RawMovementInput.y);
    }
    public void JumpInputFunction()
    {
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.Space) && playerData.amountOfJump > 1 && playerData.CurrentStamina > 3))
        { 
            JumpInput = true;
            UseStamina?.Invoke(-3);
            jumpInputStartTime = Time.time; // Record the time when the jump input was pressed
        }
    }   
    
    public void DashInputFunction()
    {
        // Handle Dash input, and ensure the dash cooldown is respected
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerData.CurrentStamina >= 7)
        {
           skillManager.ActivateDash();
           UseStamina?.Invoke(-7);
        }
    }

    public void SlideInputFunction()
    {
        if (Input.GetKeyDown(KeyCode.S) && playerData.UsageTimer >= playerData.slideCoolDown && playerData.CurrentStamina >= 3)
        {
            SlideInput = true; // Allow slide
            slideInputStartTime = Time.time; // Record the time when slide input was pressed
            playerData.UsageTimer = 0; // Reset slide cooldown timer
            UseStamina?.Invoke(-3);
        }
    }

    public void RightClickFunction()
    {
        if(Input.GetKeyDown(KeyCode.Mouse1) && playerData.CurrentStamina > 10)
        {
            RightClick = true;
            righttclickInputStartTime = playerData.UsageTimer;
            UseStamina?.Invoke(-10);

            playerData.PassingTime = righttclickInputStartTime;
        }    
    }
    public void LeftClickFunction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && playerData.CurrentStamina > 2)
        {
            LeftClick = true;
            UseStamina?.Invoke(-2);
            // Update CountClick and reset TimeAttack when player starts clicking
            leftclickInputStartTime = playerData.UsageTimer;
            // If more than 2 clicks, reset CountClick for the next sequence
            playerData.PassingTime = leftclickInputStartTime;   

            // Reset LeftClick to prevent double counting

           
            
        }
        if (playerData.UsageTimer - playerData.PassingTime >= 2.5f || playerData.CountClick > 3)
        {
            playerData.CountClick = 1; // Reset lại combo
        }


    }


    // Method to reset Jump input after it's been used
    public void UseJumpInput() => JumpInput = false;

    // Method to reset Dash input after it's been used
    public void UseDashInput() => DashInput = false;

    // Method to reset Slide input after it's been used
    public void UseSlideInput() => SlideInput = false;
    //public void UseLeftClickInput() => LeftClick = false;

    // Method to check if the jump input hold time has passed, and reset it if so
    private void CheckJumpInputHoldTime()
    {
        if (Time.time >= jumpInputStartTime + inputHoldTime)
        {
            JumpInput = false; // Reset jump input if hold time has passed
        }
    }

    // Method to check if the dash input hold time has passed, and reset it if so
    private void CheckDashInputHoldTime()
    {
        if (Time.time >= dashInputStartTime + inputHoldTime)
        {
            DashInput = false; // Reset dash input if hold time has passed
        }
    }
    
    // Method to check if the slide input hold time has passed, and reset it if so
    private void CheckSlideInputHoldTime()
    {
        if (Time.time >= slideInputStartTime + inputHoldTime)
        {
            SlideInput = false; // Reset slide input if hold time has passed
        }
    }

    public void SetIsDead()
    {
        isDead = true;
    }


}
