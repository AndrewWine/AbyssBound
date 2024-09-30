using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    [SerializeField] public PlayerState CurrentState;
    public List<PlayerState> ListOfStates;
    public BlackBoard blackBoard;
    public Player player;
    public PlayerData playerData;  
    public PlayerState StartingState;
    
    private void Awake()
    {
        
        PlayerState[] playerState = FindObjectsOfType<PlayerState>();
        ListOfStates = playerState.ToList();
        foreach(PlayerState state in ListOfStates)
        {
            state.Initialzie(blackBoard, player, this, playerData);
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


  
    public void Initialize(PlayerState startingState )
    {
        CurrentState = startingState;
        CurrentState.Enter();
    }

    public void ChangeState(PlayerState newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }

}
