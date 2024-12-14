using UnityEditor;
using UnityEngine;

public class ItemDatabaseGenerator : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("Tools/Generate Item Database")]
    private static void GenerateDatabase()
    {
        // Find all ItemData assets
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

        // Save the database to Assets
        string savePath = "Assets/ItemDatabase.asset";
        AssetDatabase.CreateAsset(database, savePath);
        AssetDatabase.SaveAssets();

        Debug.Log($"Item Database generated and saved to {savePath}");
    }
#endif
}
