using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
public class SaveManager : MonoBehaviour
{
    public static Action LoadItemSaved;
    public GameData gameData;
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
        HasNoSavedData();
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

        Debug.Log("Load item inventory: " + gameData.inventory);

        Debug.Log("Load equipmentID: " + gameData.equipmentID);
        LoadItemSaved?.Invoke();


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
            Debug.Log("Game was saved! aaaaa");
        }
        dataHandler.Save(gameData);
        Debug.Log("Game was saved!");
    }

    public void QuitGame()
    {
        SaveGame(); // Lưu game trước khi thoát

        // Nếu đang chạy trong Editor, dừng game
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    // Nếu game đã build, thoát ứng dụng
    Application.Quit();
#endif
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


