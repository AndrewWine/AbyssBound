using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UI : MonoBehaviour
{
    [Header("End Screen")]
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject DarkScreen;
    [SerializeField] private GameObject restartButton;
    [SerializeField] public UI_FadeScreen fadeScreen;
    [Space]


    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;


    public Action PressRestartBtn;

    public UI_StatTooltip statTooltip;
    public UI_ItemTooltip itemTooltip;
    public UI_CraftWindow craftWindow;
    public UI_SKillToolTip sKillToolTip;
    private void Awake()
    {
       
        if (itemTooltip == null)
        {
            // Tự động tìm GameObject có Item_Tooltip
            itemTooltip = FindObjectOfType<UI_ItemTooltip>();

            if (itemTooltip == null)
            {
                Debug.Log("Không tìm thấy Item Tooltip trong scene!");
            }
            else
            {
                Debug.Log("Item Tooltip đã được tự động gán.");
            }
        }
        fadeScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        // Bật inGameUI
        SwitchTo(inGameUI);

        // Đảm bảo endText bị tắt khi bắt đầu game
        if (endText != null)
        {
            endText.SetActive(false);
        }

        // Gọi hiệu ứng fadeIn
        fadeScreen.FadeIn();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) { SwitchWithKeyTo(characterUI); }
        if (Input.GetKeyDown(KeyCode.B)) { SwitchWithKeyTo(craftUI); }
        if (Input.GetKeyDown(KeyCode.K)) { SwitchWithKeyTo(skillTreeUI); }
        if (Input.GetKeyDown(KeyCode.O)) { SwitchWithKeyTo(optionsUI); }


    }

    public void SwitchWithKeyTo(GameObject _menu)
    {
        if(_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
             CheckForInGameUI();
            return;
        }
        SwitchTo(_menu);
    }



    public void SwitchTo(GameObject _menu)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject child = transform.GetChild(i).gameObject;
            bool isFadeScreen = child.GetComponent<UI_FadeScreen>() != null;

            // Không tắt inGameUI và fadeScreen
            if (child != inGameUI && !isFadeScreen)
            {
                child.SetActive(false);
            }
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }
    }


    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                return;
            }
        }
        SwitchTo(inGameUI);
    }

    public void SwithOnEndScreen()
    {
        // Chạy fadeOut
        fadeScreen.FadeOut();
        // Hiển thị endText sau hiệu ứng fadeOut
        StartCoroutine(EndScreenCoroutine());

    }

    IEnumerator EndScreenCoroutine()
    {
        // Chờ 1 giây để hiệu ứng fadeOut hoàn tất
        yield return new WaitForSeconds(1);
        // Hiển thị endText
        endText.SetActive(true);
        yield return new WaitForSeconds(2);
        restartButton.SetActive(true);
    }

    public void RestartGameButton() => PressRestartBtn?.Invoke();
}
