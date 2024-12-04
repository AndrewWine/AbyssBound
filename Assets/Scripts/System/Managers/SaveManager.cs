using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class SaveManager : MonoBehaviour
{
    public GameData gameData;
    public UI ui;
    public GameManager gameManager;
    [SerializeField] private string fileName;

    [Header("Observer")]
    public Action<bool> hasFileSave;
    public static SaveManager instance;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [ContextMenu("Delete save file")]
    public void DeleteSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        dataHandler.Delete();
    }



    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath,fileName);
        saveManagers = FindAllSaveManagers();
        LoadGame();
    }
    public void NewGame()
    {

    }

    private void OnEnable()
    {
        gameManager.NotifySaveGame += SaveGame;
    }

    private void OnDisable()
    {
        gameManager.NotifySaveGame -= SaveGame;

    }
    public void LoadGame()
    {
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No save data found! Starting a new game.");
            NewGame(); // Hoặc thêm logic khởi tạo game mới ở đây nếu cần
            return; // Tránh lỗi NullReferenceException khi tiếp tục
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }

        Debug.Log("Load Currency: " + gameData.AbyssEssence);
    }


    public void SaveGame()
    {
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
            Debug.Log("Game was saved! aaaaa");
        }
        dataHandler.Save(gameData);
        Debug.Log("Game was saved!");
    }

    public void QuitGame()
    {
        SaveGame();
        Application.Quit();
    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveManagers()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>().OfType<ISaveManager>();
        return new List<ISaveManager>(saveManagers);
    }

    public bool HasNoSavedData()
    {
        if(dataHandler.Load() != null)
        {
            hasFileSave?.Invoke(true);
            Debug.Log("Load file save");
            return true;
        }

        else
            hasFileSave?.Invoke(false);
            
            return false;

    }
}


