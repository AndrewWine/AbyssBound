using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    [SerializeField] private GameObject canvasUI; // Tham chiếu đến Canvas UI

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (canvasUI != null)
            {
                // Đổi trạng thái SetActive của Canvas UI
                canvasUI.SetActive(!canvasUI.activeSelf);
            }
        }
    }
}
