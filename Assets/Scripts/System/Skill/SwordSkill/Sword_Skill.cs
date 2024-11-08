
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

    [Header("Aim dots")]
    [SerializeField] private int numberOfDots;
    [SerializeField] private float spaceBetweenDots;
    [SerializeField] private GameObject dotPrefab;
    [SerializeField] private Transform dotsParent;

    [Header("Game Object")]
    private ObjectPool<SwordSkill_Controller> swordPool; // Tham chiếu đến pool kiếm
    public Player player;
    private Vector2 finalDir;
    private GameObject[] dots;

    protected override void Awake()
    {

   

    }
    protected override void Start()
    {
       
        SwordSkill_Controller swordController = swordPrefab.GetComponent<SwordSkill_Controller>();
        if (swordController == null)
        {
            Debug.LogError("SwordSkill_Controller component not found on the swordPrefab!");
        }

        // Initialize the object pool for swords
        swordPool = new ObjectPool<SwordSkill_Controller>(swordController, 1);
        GenerateDots();
        base.Start(); // Nếu có kế thừa lớp base
    }

    protected override void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPosition(i * spaceBetweenDots);
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

            // Đặt kiếm về vị trí người chơi
            // Trong phương thức CreateSword
            GameObject modelObject = player.transform.Find("AttackCheck").gameObject; // Thay "Model" bằng tên chính xác của GameObject
            newClone.transform.position = modelObject.transform.position;

            // Kích hoạt kiếm
            newClone.gameObject.SetActive(true);

            // Tính toán finalDir với launchForce
            // Trong phương thức CreateSword, thêm hệ số để kiếm di chuyển theo phương x nhanh hơn
            Vector2 lastDotPosition = dots[dots.Length - 1].transform.position;
            finalDir = (lastDotPosition - (Vector2)player.transform.position).normalized * launchForce.magnitude;

            // Nhân thêm hệ số vào finalDir.x để tăng tốc độ di chuyển theo trục x
            finalDir = new Vector2(finalDir.x * 2.5f, finalDir.y); // Tăng tốc theo phương x
            newClone.SetupSword(finalDir, swordGravity,swordLifetime);


            // Debug để kiểm tra vận tốc
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

        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].SetActive(isActive);
        }
    }

    private void GenerateDots()
    {

        dots = new GameObject[numberOfDots];
        for (int i = 0; i < numberOfDots; i++)
        {
            dots[i] = Instantiate(dotPrefab, player.transform.position, Quaternion.identity, dotsParent);
            dots[i].SetActive(false);
        }

    }

    private Vector2 DotsPosition(float t)
    {
        Vector2 position = (Vector2)player.transform.position + new Vector2(
            AimDirection().normalized.x * launchForce.x,
            AimDirection().normalized.y * launchForce.y) * t + 0.5f * (Physics2D.gravity * swordGravity) * (t * t);
        return position;
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
   
        // Đặt lại thời gian cooldown
        base.UseSkill();
    }
}