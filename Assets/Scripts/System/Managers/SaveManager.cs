using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SaveManager : MonoBehaviour
{
    public static Action LoadItemSaved;
    public static Action resetPlayerData;
    public GameData gameData;
    public GameManager gameManager;

    [Header("File Names")]
    [SerializeField] private string saveFileName = "gameDataSoul"; // For saving game data
    [SerializeField] private string defaultSaveFileName = "gameDefaultDataSoul"; // For loading default data

    [Header("Observer")]
    public Action<bool> hasFileSave;
    public static SaveManager instance;
    private List<ISaveManager> saveManagers;
    private FileDataHandler dataHandler;

    [ContextMenu("Delete save file")]
    public void DeleteSaveData()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, saveFileName);
        dataHandler.Delete();
    }

    private void Start()
    {
        dataHandler = new FileDataHandler(Application.persistentDataPath, saveFileName);
        saveManagers = FindAllSaveManagers();
        LoadGame();
        HasNoSavedData();
    }

    public void NewGame()
    {
        resetPlayerData?.Invoke();
        LoadDefaultGameData(); // Load the default game data when starting a new game
    }

    private void OnEnable()
    {
        gameManager.NotifySaveGame += SaveGame;
        CheckPoint.NotifySaveGameatCheckPoint += SaveGame;
    }

    private void OnDisable()
    {
        gameManager.NotifySaveGame -= SaveGame;
        CheckPoint.NotifySaveGameatCheckPoint -= SaveGame;
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
        Debug.Log("Load item inventory: " + gameData.inventory);
        Debug.Log("Load equipmentID: " + gameData.equipmentID);
        LoadItemSaved?.Invoke();
    }

    public void LoadDefaultGameData()
    {
        // Load from the default save file (`gameDefaultDataSoul`)
        dataHandler = new FileDataHandler(Application.persistentDataPath, defaultSaveFileName);
        gameData = dataHandler.Load();

        if (gameData == null)
        {
            Debug.Log("No default save data found! Initializing new game data.");
            gameData = new GameData(); // Initialize with default values if needed
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }

        Debug.Log("Loaded Default Data for New Game.");
    }

    public void SaveGame()
    {
        if (saveManagers == null || saveManagers.Count == 0)
        {
            Debug.LogError("saveManagers is null or empty. Ensure it is properly initialized.");
        }

        foreach (ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        // Save to the normal save file (`gameDataSoul`)
        dataHandler = new FileDataHandler(Application.persistentDataPath, saveFileName);
        dataHandler.Save(gameData);

        Debug.Log("Game was saved!");
    }

    public void QuitGame()
    {
        SaveGame(); // Lưu game trước khi thoát

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
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
        if (dataHandler.Load() != null)
        {
            hasFileSave?.Invoke(true);
            Debug.Log("Load file save");
            return true;
        }
        else
        {
            hasFileSave?.Invoke(false);
            return false;
        }
    }
}
