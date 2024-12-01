using System.Data.Common;
using UnityEngine;

public class CloneAttack : Skill
{
    [Header("Clone Information")]
    [SerializeField] private GameObject clonePrefab;  // Clone prefab
    [SerializeField] private float cloneDuration;      // Duration of clone
    [SerializeField] private bool canAttack;           // Can clone attack?

    private ObjectPool<CloneSkill_Controller> clonePool;

    protected override void Start()
    {
        // Initialize the object pool
        cooldown = 2;
        cooldownTimer = 0;
        clonePool = new ObjectPool<CloneSkill_Controller>(clonePrefab.GetComponent<CloneSkill_Controller>(), 1); // Initial pool size is 1
    }

    // Method to create a clone
    public void CreateClone(Vector3 _clonePosition)
    {
        CloneSkill_Controller newClone = clonePool.Get() as CloneSkill_Controller;

        if (newClone != null)
        {
            newClone.SetPool(clonePool);  // Ensure pool is set here
            newClone.transform.position = _clonePosition;
            newClone.gameObject.SetActive(true);
            newClone.SetupClone(newClone.transform, cloneDuration, canAttack); // Ensure cloneDuration is set correctly
        }
        else
        {
            Debug.LogError("Cannot get clone from pool!");
        }
    }

    protected override void Awake()
    {
        cooldown = 2;
        cooldownTimer = 0;
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void ActivateSkill()
    {
        UseSkill();


    }

    public override void UseSkill()
    {
        base.UseSkill();
    }
}
