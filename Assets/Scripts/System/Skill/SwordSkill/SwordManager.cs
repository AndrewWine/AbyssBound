using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public PlayerBlackBoard blackBoard;
    public CircleCollider2D AttackCheck;

    private void Awake()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            blackBoard = playerObject.GetComponent<PlayerBlackBoard>();
            AttackCheck = GetComponent<CircleCollider2D>();
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng Player để gán PlayerBlackBoard.");
        }

       
    }


}
