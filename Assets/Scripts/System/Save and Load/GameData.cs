using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GameData 
{
    public float AbyssEssence;
    public SerializableDictionary<string, int> inventory;
    public List<string> equipmentID;

    public SerializableDictionary<string, bool> checkpoints;
    public string closestCheckpointID;
    public float lostCurrencyX;
    public float lostCurrencyY;
    public float lostCurrencyAmount;
    public GameData()
    {
        this.lostCurrencyX = 0;
        this.lostCurrencyY = 0;
        this.lostCurrencyAmount = 0;
        this.AbyssEssence = 0;
        inventory = new SerializableDictionary<string, int>();
        equipmentID = new List<string>();
        closestCheckpointID = string.Empty;
        checkpoints = new SerializableDictionary<string, bool>();
    }
}
