using UnityEngine;

public class Sword_Skill : Skill
{
    [Header("Skill info")]
    [SerializeField] private GameObject swordPrefab;
    [SerializeField] private Vector2 launchForce;
    [SerializeField] private float swordGravity;
    [SerializeField] private float swordLifetime;

    [Header("Cooldown")]
    [SerializeField] private float throwingCooldown;
    private float throwingCooldownTimer;

    [Header("Aim dot")]
    [SerializeField] private GameObject dotPrefab;

    [Header("Transform")]
    [SerializeField] private Transform dotsParent;
    public Transform attackcheck;

    [Header("Game Object")]
    private ObjectPool<SwordSkill_Controller> swordPool;
    public Player player;
    private Vector2 finalDir;

    protected override void Awake()
    {
        SwordSkill_Controller swordController = swordPrefab.GetComponent<SwordSkill_Controller>();
        if (swordController == null)
        {
            Debug.LogError("SwordSkill_Controller component not found on the swordPrefab!");
        }

        // Initialize the object pool for swords
        swordPool = new ObjectPool<SwordSkill_Controller>(swordController, 1);

        // Ensure dotsParent is inactive at the start
        DotsActive(false);

        base.Start();
    }

    protected override void Update()
    {
        // Khi bấm Q, di chuyển dotParent theo chuột
        if (Input.GetKey(KeyCode.Q))
        {
            // Di chuyển dotParent theo chuột
            if (dotsParent != null)
            {
                dotsParent.position = GetMousePosition();
            }

            // Hiện dotParent nếu nó chưa được hiển thị
            DotsActive(true);
        }

        // Khi thả Q và có thể sử dụng skill
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
            Vector2 lastDotPosition = dotsParent.position;
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

        // Tắt dotParent sau khi tạo kiếm
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
        // Kích hoạt hoặc tắt dotParent (chứa tất cả các dotPrefab)
        if (dotsParent != null)
        {
            dotsParent.gameObject.SetActive(isActive);
        }
    }

    private Vector2 GetMousePosition()
    {
        // Lấy vị trí chuột trong không gian thế giới
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
