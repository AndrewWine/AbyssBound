using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class PlayerManager : MonoBehaviour,ISaveManager
{
    [Header("Component")]
    public PlayerData playerData;
    public Player player;

    [Header("Action Observer")]
    public Action NotifyUpdateCurrency;

    //Quản lý sự tăng giảm soul của player
    private void OnEnable()
    {
        EnemyStat.DropAbyssEssence += OnCurrencyChange;
        LostCurrencyController.pickedCurrency += OnCurrencyChange;
    }

    private void OnDisable()
    {
        EnemyStat.DropAbyssEssence -= OnCurrencyChange;
        LostCurrencyController.pickedCurrency -= OnCurrencyChange;


    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    public void OnCurrencyChange(float amount)
    {
        playerData.AbyssEssence += amount; // Hoàn lại AbyssEssence
        NotifyUpdateCurrency?.Invoke(); // Cập nhật UI
    }

    public void LoadData(GameData _data)
    {
        this.playerData.AbyssEssence = _data.AbyssEssence;
    }

    public void SaveData(ref GameData _data)
    {
        if( _data == null )
            return;
        _data.AbyssEssence = this.playerData.AbyssEssence;
    }
}
