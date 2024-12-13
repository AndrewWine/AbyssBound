using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName = "MainScene";
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;
    [SerializeField] public SaveManager saveManager;


    private void Awake()
    {
        saveManager.hasFileSave += SetUpButtonContinue;


    }

    private void OnDisable()
    {
        saveManager.hasFileSave -= SetUpButtonContinue;

    }
    public void SetUpButtonContinue(bool check)
    {
        if(check)
        {
            continueButton.SetActive (true);
        }
        else
            continueButton.SetActive (false);
    }
    public void ContinueGame()
    {
        StartCoroutine(LoadScenceWithFadeEffect(1.5f));
    }

    public void NewGame()
    {
        saveManager.DeleteSaveData();
        StartCoroutine(LoadScenceWithFadeEffect(1.0f));
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
    }

    IEnumerator LoadScenceWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(_delay);
        SceneManager.LoadScene(sceneName);
    }
}
