using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerData playerData; // Reference to player data scriptable object
    public Player player;

    // Input variables
    public Vector2 RawMovementInput { get; private set; }
    public int NormInputX { get; private set; }
    public int NormInputY { get; private set; }
    public bool JumpInput { get; private set; }
    public bool JumpInputStop { get; private set; }
    public bool DashInput { get; private set; }
    public bool DashInputStop { get; private set; }
    public bool SlideInput { get; private set; }
    public bool LeftClick {  get; set; }

    // Input hold times
    [SerializeField] private float inputHoldTime = 0.2f; // How long to hold the input
    private float jumpInputStartTime;
    private float dashInputStartTime;
    private float slideInputStartTime;
    private float leftclickInputStartTime = 0;


    void Update()
    {
        // Check hold times for inputs
        CheckJumpInputHoldTime();
        CheckDashInputHoldTime();
        CheckSlideInputHoldTime();

        OldInputFunction();
        JumpInputFunction();
        DashInputFunction();
        SlideInputFunction();
        LeftClickFunction();

        


        playerData.UsageTimer += Time.deltaTime;
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
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) && playerData.amountOfJump > 1)
        {
            JumpInput = true;
            jumpInputStartTime = Time.time; // Record the time when the jump input was pressed
        }
    }   
    
    public void DashInputFunction()
    {
        // Handle Dash input, and ensure the dash cooldown is respected
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerData.UsageTimer >= playerData.dashCoolDown)
        {
            DashInput = true; // Allow dash
            dashInputStartTime = Time.time; // Record the time when dash input was pressed
            playerData.UsageTimer = 0; // Reset dash cooldown timer

        }
    }

    public void SlideInputFunction()
    {
        if (Input.GetKeyDown(KeyCode.S) && playerData.UsageTimer >= playerData.slideCoolDown)
        {
            SlideInput = true; // Allow slide
            slideInputStartTime = Time.time; // Record the time when slide input was pressed
            playerData.UsageTimer = 0; // Reset slide cooldown timer

        }
    }

    public void LeftClickFunction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            LeftClick = true;
            // Update CountClick and reset TimeAttack when player starts clicking
            leftclickInputStartTime = playerData.UsageTimer;

            Debug.Log("countclick" +  playerData.CountClick);
            if(playerData.CountClick > 3 || leftclickInputStartTime - playerData.PassingTime >= 1.5f)
            {
                playerData.CountClick = 0;
            }

            playerData.CountClick++;
            // If more than 2 clicks, reset CountClick for the next sequence

            playerData.PassingTime = leftclickInputStartTime;
        }
    }

    // Method to reset Jump input after it's been used
    public void UseJumpInput() => JumpInput = false;

    // Method to reset Dash input after it's been used
    public void UseDashInput() => DashInput = false;

    // Method to reset Slide input after it's been used
    public void UseSlideInput() => SlideInput = false;
    public void UseLeftClickInput() => LeftClick = false;

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

 

  
}
