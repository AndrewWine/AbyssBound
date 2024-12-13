using UnityEditor;
using UnityEngine;

public class ItemDatabaseGenerator : MonoBehaviour
{
    [MenuItem("Tools/Generate Item Database")]
    private static void GenerateDatabase()
    {
        // Tìm tất cả các ItemData
        string[] assetPaths = AssetDatabase.FindAssets("t:ItemData");
        ItemDatabase database = ScriptableObject.CreateInstance<ItemDatabase>();

        foreach (string guid in assetPaths)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);

            if (item != null)
            {
                database.itemDataList.Add(item);
            }
        }

        // Lưu Database vào Assets
        string savePath = "Assets/ItemDatabase.asset";
        AssetDatabase.CreateAsset(database, savePath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Item Database generated and saved to {savePath}");
    }
}
