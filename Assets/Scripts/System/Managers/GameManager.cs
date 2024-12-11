using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class GameManager : MonoBehaviour, ISaveManager
{
    public UI ui;
    public PlayerData playerData;
    private Transform player;
    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private string closestCheckpointId;
    [Header("Lost currency")]
    [SerializeField] private GameObject lostCurrencyPrefab;
    [SerializeField] private float lostCurrencyX;
    [SerializeField] private float lostCurrencyY;

    public float lostCurrencyAmount;
    public Action NotifySaveGame;
    private void Awake()
    {
        ui.PressRestartBtn += RestartScene;
        checkPoints = FindObjectsOfType<CheckPoint>();
        player = GameObject.Find("Player").GetComponent<Transform>();
    }

    private void OnDisable()
    {
        ui.PressRestartBtn -= RestartScene;
    }
    public void RestartScene()
    {
        NotifySaveGame?.Invoke();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Debug.Log("Restart scene");
    }


    public void LoadData(GameData _data) => StartCoroutine(LoadWithDelay(_data));


    private void LoadCheckpoints(GameData _data)
    {
        foreach (KeyValuePair<string, bool> pair in _data.checkpoints)
        {
            foreach (CheckPoint checkPoint in checkPoints)
            {
                if ((checkPoint.id == pair.Key) && pair.Value == true)
                {
                    checkPoint.ActivateCheckpoint();

                }
            }
        }
    }

    private void LoadLostCurrency(GameData _data)
    {
        float lostCurrencyAmount = _data.lostCurrencyAmount;
        // Lưu AbyssEssence hiện tại vào dữ liệu GameData
        _data.lostCurrencyAmount = playerData.AbyssEssence;

        // Gán giá trị này vào các biến tạm để tạo LostCurrency
        lostCurrencyAmount = _data.lostCurrencyAmount;
        lostCurrencyX = _data.lostCurrencyX;
        lostCurrencyY = _data.lostCurrencyY;

       

        if (lostCurrencyAmount > 0)
        {
            GameObject newLostCurrency = Instantiate(lostCurrencyPrefab, new Vector3(lostCurrencyX, lostCurrencyY), Quaternion.identity);

            // Lưu giá trị AbyssEssence bị mất trong LostCurrencyController
            LostCurrencyController lostCurrencyController = newLostCurrency.GetComponent<LostCurrencyController>();
            lostCurrencyController.lostAbyssEssence = lostCurrencyAmount;

            // Đặt lại AbyssEssence của người chơi về 0
            playerData.AbyssEssence = 0;
        }
    }


    IEnumerator LoadWithDelay(GameData _data)
    {
        yield return new WaitForSeconds(0.1f);
        LoadClosesetCheckpoint(_data);
        LoadLostCurrency(_data);
        LoadCheckpoints(_data);

    }

    public void SaveData(ref GameData _data)
    {
        if (_data == null)
            return;
        _data.lostCurrencyAmount = lostCurrencyAmount;
        _data.lostCurrencyX = player.transform.position.x;
        _data.lostCurrencyY = player.transform.position.y;

        if(FindClosestCheckPoint() != null) 
            _data.closestCheckpointID = FindClosestCheckPoint().id;

        _data.checkpoints.Clear();
       foreach(CheckPoint checkPoint in checkPoints)
        {
            _data.checkpoints.Add(checkPoint.id, checkPoint.activationStatus);
        }
    }
    private void LoadClosesetCheckpoint(GameData _data)
    {
        if (_data.closestCheckpointID == null)
            return;

        closestCheckpointId = _data.closestCheckpointID;

        foreach (CheckPoint checkPoint in checkPoints)
        {
            if (closestCheckpointId == checkPoint.id)
                player.transform.position = checkPoint.transform.position;

        }
    }

    private CheckPoint FindClosestCheckPoint()
    {
        float closestDistance = Mathf.Infinity;
        CheckPoint closestCheckpoint = null;
        foreach(var checkPoint in checkPoints)
        {
            float distanceToCheckpoint = Vector2.Distance(player.transform.position, checkPoint.transform.position);
                if(distanceToCheckpoint < closestDistance && checkPoint.activationStatus == true)
                {
                    closestDistance = distanceToCheckpoint;
                    closestCheckpoint = checkPoint;
                }
        }
        return closestCheckpoint;
    }
}
