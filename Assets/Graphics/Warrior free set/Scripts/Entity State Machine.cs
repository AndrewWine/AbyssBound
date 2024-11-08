using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityStateMachine<TBlackboard> : MonoBehaviour where TBlackboard : EntityBlackboard
{
    [SerializeField] public State<TBlackboard> CurrentState;
    public List<State<TBlackboard>> listOfStates = new(); // Riêng cho mỗi Enemy
    public TBlackboard blackboard;
    public State<TBlackboard> StartingState;


    private void Awake()
    {

        State<TBlackboard>[] states = blackboard.statesParent.GetComponentsInChildren<State<TBlackboard>>();
        listOfStates = states.ToList();
        foreach (var state in listOfStates)
        {
            state.Initialzie(blackboard, this);
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



    public void Initialize(State<TBlackboard> startingState)
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(State<TBlackboard> newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
