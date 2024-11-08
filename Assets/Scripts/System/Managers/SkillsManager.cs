using UnityEngine;

public class SkillsManager : MonoBehaviour
{

    [Header("Component")]
    public PlayerInputHandler playerInputHandler;
    [SerializeField] PlayerStateMachine stateMachine;
    public PlayerBlackBoard blackBoard;
    public Player player;
    public SwordSkill_Controller skillController;
    [Header("Object Pool")]
    public CloneSkill_Controller clonePrefab; // Drag and drop prefab into here from Inspector
    public SwordSkill_Controller swordPrefab; // Sword prefab for pooling

    private ObjectPool<CloneSkill_Controller> clonePool;

    private ObjectPool<SwordSkill_Controller> swordPool;

    #region Skills
    public Dash_Skill dash { get; private set; }
    public CloneAttack clone { get; private set; }
    public Sword_Skill sword_Skill { get; private set; }
    #endregion

    private void Awake()
    {
        dash = GetComponent<Dash_Skill>();
        clone = FindObjectOfType<CloneAttack>();
        player = FindObjectOfType<Player>();
        sword_Skill = FindObjectOfType<Sword_Skill>();

        // Initialize pool with clone prefab and initial size
        clonePool = new ObjectPool<CloneSkill_Controller>(clonePrefab, 1); // Initial pool size is 1
        swordPool = new ObjectPool<SwordSkill_Controller>(swordPrefab, 1); // Initial pool size is 1
      
    }
    private void Start()
    {
        skillController.CallCatchState += ChangeCatchState;
    }
    private void OnDestroy()
    {
        skillController.CallCatchState -= ChangeCatchState;

    }
   
    public void ChangeCatchState()
    {
        stateMachine.ChangeState(blackBoard.catchSword);
    }
    // Method to activate dash skill   
    public void ActivateDashCloneAttack()
    {
        if (dash.CanUseSkill())
        {
            playerInputHandler.DashInput = true; // Set DashInput
            dash.ActivateSkill();
            stateMachine.ChangeState(blackBoard.DashState);

            // Create clone based on the playerPos's position
            CloneSkill_Controller cloneInstance = clonePool.Get();

            if (cloneInstance != null)
            {
                cloneInstance.transform.position = player.transform.position; // Place clone at playerPos's position
                cloneInstance.SetPool(clonePool); // Set the pool reference
                clone.CreateClone(player.transform.position); // Call CreateClone on the CloneAttack instance
                clone.ActivateSkill();

            }
            else
            {
                Debug.LogError("Cannot get clone from pool!");
            }
        }
        else
        {
            playerInputHandler.DashInput = false;
        }
    }

    public void ActivateDash()
    {
        if (dash.CanUseSkill())
        {
            Debug.Log("Dax dash");
            playerInputHandler.DashInput = true; // Set DashInput
            dash.ActivateSkill();
            stateMachine.ChangeState(blackBoard.DashState);
        }
        else
        {
            playerInputHandler.DashInput = false;
        }
    }

    public void ActivateThrowSword()
    {
        SwordSkill_Controller cloneInstance = swordPool.Get();
        cloneInstance.transform.position = player.transform.position; // Place clone at playerPos's position
        cloneInstance.SetPool(swordPool); // Set the pool reference
        sword_Skill.CreateSword();
        Debug.Log("Active thanh cong");
    }
}
