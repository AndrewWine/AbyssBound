using System;
using UnityEditor.Experimental.GraphView;
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

    public Action<int> UseMana;
    public Action<int> UseStamina;
    public Action DashSkillCoolDown;
    public Action CloneSkillCoolDown;
    public Action ThrowSwordCooolDown;

    #region Skills
    public Dash_Skill dash { get; private set; }
    public CloneAttack clone { get; private set; }
    public Sword_Skill sword_Skill { get; private set; }
    #endregion

    private void Awake()
    {
        dash = GetComponent<Dash_Skill>();
        clone = GetComponent<CloneAttack>();
        player = FindObjectOfType<Player>();
        sword_Skill = FindObjectOfType<Sword_Skill>();

        // Initialize pool with clone prefab and initial size
        clonePool = new ObjectPool<CloneSkill_Controller>(clonePrefab, 1); // Initial pool size is 1
        swordPool = new ObjectPool<SwordSkill_Controller>(swordPrefab, 0); // Initial pool size is 1
      
    }
    private void OnEnable()
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
    public void ActivateCloneAttack()
    {
        if (clone.CanUseSkill())
        {
          
            // Create clone based on the playerPos's position
            CloneSkill_Controller cloneInstance = clonePool.Get();

            if (cloneInstance != null)
            {
                cloneInstance.transform.position = player.transform.position; // Place clone at playerPos's position
                cloneInstance.SetPool(clonePool); // Set the pool reference
                clone.CreateClone(player.transform.position ); // Call CreateClone on the CloneAttack instance
                clone.ActivateSkill();
                stateMachine.ChangeState(blackBoard.fallBack);
                UseMana?.Invoke(-1);
                CloneSkillCoolDown?.Invoke();
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
            playerInputHandler.DashInput = true; // Cập nhật DashInput
            dash.ActivateSkill();
            stateMachine.ChangeState(blackBoard.DashState);

            // Kích hoạt sự kiện UseStamina
            UseStamina?.Invoke(-10); // Ví dụ là lượng stamina sử dụng, cần truyền đúng giá trị
            DashSkillCoolDown?.Invoke();
        }
        else
        {
            playerInputHandler.DashInput = false;
        }
    }



    public void ActivateThrowSword()
    {

        sword_Skill.ActivateSkill();
        ThrowSwordCooolDown?.Invoke();
        UseStamina?.Invoke(-5);
        Debug.Log("Active thanh cong");

    }
}
