using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityBlackboard : MonoBehaviour
{
    public GameObject statesParent;

    [Header("Check variable")]
    public bool isWall;
    public bool isGrounded;
    public int FacingDirection;

    [Header("Transform")]
    [SerializeField] public Transform groundCheck;
    [SerializeField] public Transform wallCheck;
    [SerializeField] public Transform PlayerCheck;
}


