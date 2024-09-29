using UnityEngine;

[CreateAssetMenu(fileName = "newEnemyData", menuName = "Data/Enemy Data/ Base Data")]
public class EnemyData : ScriptableObject
{
    [Header("Moving")]
    public float MovementSpeed = 5;

    [Header("Index")]
    public float Hp;
    public float Damage;
    public float Armor;

    [Header("Check Variables")]
    public float groundCheckDistance = 0.3f;
    public float WallCheckDistance;
    public LayerMask whatIsGround;
}
