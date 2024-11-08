using System;
using UnityEngine;

public class PlayerManagers : MonoBehaviour
{
    // Sự kiện sẽ phát ra PlayerManager (cụ thể là transform)
    public static Action<Transform> OnPlayerInitialized;

    private void Awake()
    {
        // Khi PlayerManager khởi tạo, phát sự kiện cùng với Transform của nó
        OnPlayerInitialized?.Invoke(transform);
    }
}
