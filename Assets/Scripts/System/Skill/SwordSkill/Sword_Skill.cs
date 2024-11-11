using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float swordLifetime; // Thời gian tồn tại của kiếm

    [Header("Cooldown")]
    [SerializeField] private float throwingCooldown;
    private float throwingCooldownTimer;

    [Header("Aim dot")]
    [SerializeField] private GameObject dotPrefab;

    [Header("Transform")]
    [SerializeField] private Transform dotsParent;
    public Transform attackcheck;

    [Header("Game Object")]
    private ObjectPool<SwordSkill_Controller> swordPool; // Tham chiếu đến pool kiếm
    public Player player;
    private Vector2 finalDir;
    private GameObject dot; // Chỉ cần một dot

    protected override void Awake()
    {
        SwordSkill_Controller swordController = swordPrefab.GetComponent<SwordSkill_Controller>();
        if (swordController == null)
        {
            Debug.LogError("SwordSkill_Controller component not found on the swordPrefab!");
        }

        // Initialize the object pool for swords
        swordPool = new ObjectPool<SwordSkill_Controller>(swordController, 1);

        // Generate a single dot
        GenerateDot();

        base.Start(); // Nếu có kế thừa lớp base
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            // Update the dot's position to follow the mouse cursor
            if (dot != null)
            {
                dot.transform.position = GetMousePosition();
            }
        }

        if (Input.GetKeyUp(KeyCode.Q) && CanUseSkill())
        {
            finalDir = new Vector2(AimDirection().normalized.x * launchForce.x, AimDirection().normalized.y * launchForce.y);
            UseSkill();
        }
    }

    public void CreateSword()
    {
        SwordSkill_Controller newClone = swordPool.Get() as SwordSkill_Controller;
        if (newClone != null)
        {
            newClone.SetPool(swordPool);

            // Set the sword's position to the player's attack position
            newClone.transform.position = attackcheck.transform.position;

            // Activate the sword
            newClone.gameObject.SetActive(true);

            // Calculate final direction with launch force
            Vector2 lastDotPosition = dot.transform.position;
            finalDir = (lastDotPosition - (Vector2)player.transform.position).normalized * launchForce.magnitude;

            // Adjust the X direction to make the sword move faster on the X axis
            finalDir = new Vector2(finalDir.x * 2.5f, finalDir.y); // Increase speed along the X axis
            newClone.SetupSword(finalDir, swordGravity, swordLifetime);

            // Debug to check the sword's direction
            Debug.Log("Sword Direction: " + finalDir);
        }
        else
        {
            Debug.Log("Bạn chỉ có 1 kiếm");
        }

        DotsActive(false);
    }

    public Vector2 AimDirection()
    {
        Vector2 playerPosition = player.transform.position;
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = mousePosition - playerPosition;

        return direction;
    }

    public void DotsActive(bool isActive)
    {
        // In this case, we're only using one dot, so we can activate/deactivate it.
        if (dot != null)
        {
            dot.SetActive(isActive);
        }
    }

    private void GenerateDot()
    {
        // Instantiate only one dot and set it to be inactive initially
        dot = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
        dot.SetActive(false); // Initially inactive
    }

    private Vector2 GetMousePosition()
    {
        // Get the position of the mouse in the world space
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        return mousePosition;
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void ActivateSkill()
    {
        base.ActivateSkill();
    }

    public override void UseSkill()
    {
        // Reset cooldown time
        base.UseSkill();
    }
}
