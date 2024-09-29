using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] public EnemyState CurrentState;
    public List<EnemyState> ListOfStates;
    public Entity entity;
    public Enemy enemy;
    public EnemyData enemyData;
    public EnemyState StartingState;

    private void Awake()
    {

        EnemyState[] EnemyState = FindObjectsOfType<EnemyState>();
        ListOfStates = EnemyState.ToList();
        foreach (EnemyState state in ListOfStates)
        {
            state.Initialzie(entity, enemy, this, enemyData);
        }
        CurrentState = StartingState;
        CurrentState.Enter();

    }
    public void Update()
    {
        CurrentState.LogicUpdate();
    }
    private void FixedUpdate()
    {
        CurrentState.PhysicUpdate();
    }



    public void Initialize(EnemyState startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(EnemyState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
