using UnityEditor;
using UnityEngine;

public class AutoFillDatabase : Editor
{
    [MenuItem("Tools/Auto Fill Item Database")]
    public static void FillItemDatabase()
    {
        // Đường dẫn tới tệp ItemDatabase
        string databasePath = "Assets/Resources/ItemDatabase.asset";

        // Tìm hoặc tạo mới ItemDatabase
        ItemDatabase itemDatabase = AssetDatabase.LoadAssetAtPath<ItemDatabase>(databasePath);
        if (itemDatabase == null)
        {
            itemDatabase = ScriptableObject.CreateInstance<ItemDatabase>();
            AssetDatabase.CreateAsset(itemDatabase, databasePath);
            Debug.Log("Created new ItemDatabase at " + databasePath);
        }

        // Tìm tất cả các ItemData trong dự án
        string[] guids = AssetDatabase.FindAssets("t:ItemData", new[] { "Assets/Resources" });

        // Clear danh sách cũ
        itemDatabase.itemDataList.Clear();

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            ItemData item = AssetDatabase.LoadAssetAtPath<ItemData>(path);

            if (item != null)
            {
                itemDatabase.itemDataList.Add(item);
                Debug.Log("Added item: " + item.name);
            }
        }

        // Lưu thay đổi
        EditorUtility.SetDirty(itemDatabase);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        Debug.Log("ItemDatabase updated successfully!");
    }
}
