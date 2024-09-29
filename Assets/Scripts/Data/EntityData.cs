using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Entity", menuName = "Data/Entity Data/ Base Data")]

public class EntityData : ScriptableObject
{
    [Header("Check Variables")]
    public float groundCheckDistance = 0.3f;
    public float WallCheckDistance;
    public LayerMask whatIsGround;
}
