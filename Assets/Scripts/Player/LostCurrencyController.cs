using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LostCurrencyController : MonoBehaviour
{
    public static Action<float> pickedCurrency;

    [Header("Abyss Essence")]
    public float lostAbyssEssence; // Giá trị AbyssEssence bị mất

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            pickedCurrency?.Invoke(lostAbyssEssence); // Gửi giá trị lưu trữ thay vì playerData
            Destroy(this.gameObject);
        }
    }
}
