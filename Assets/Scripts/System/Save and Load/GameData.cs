using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public float AbyssEssence; // Lưu trữ tài nguyên
    public SerializableDictionary<string, int> inventory; // Lưu trữ số lượng vật phẩm
    public List<string> equipmentID; // Lưu danh sách ID của trang bị

    public SerializableDictionary<string, bool> checkpoints; // Kiểm tra trạng thái các checkpoint
    public string closestCheckpointID; // ID checkpoint gần nhất
    public float lostCurrencyX; // Vị trí X nơi mất tiền
    public float lostCurrencyY; // Vị trí Y nơi mất tiền
    public float lostCurrencyAmount; // Số lượng tiền bị mất

    public SerializableDictionary<string, float> volumeSettings; // Cài đặt âm lượng

    // Constructor
    public GameData()
    {
        // Giá trị mặc định
        this.lostCurrencyX = 0;
        this.lostCurrencyY = 0;
        this.lostCurrencyAmount = 0;
        this.AbyssEssence = 0;

        // Khởi tạo các dictionary và list
        inventory = new SerializableDictionary<string, int>();
        equipmentID = new List<string>();
        checkpoints = new SerializableDictionary<string, bool>();
        volumeSettings = new SerializableDictionary<string, float>();

        closestCheckpointID = string.Empty; // Giá trị mặc định
    }
}
