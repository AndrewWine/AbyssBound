using UnityEngine;
using System;
using System.Collections.Generic;

public enum ItemType
{
    Material,
    Equipment
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemType itemtype;
    public string itemName;
    public Sprite icon;
    public static Action<ItemData> getItem;

    [Range(0, 100)]
    public float dropChance;

    public void PickUpItem()
    {
        getItem?.Invoke(this);
    }



    [Header("Major Stats")]
    public float strength; //1 point increase damage by 1 and crit.power by 1%
    public float agility;  // 1 point increase evasion by 1%, move speed 1% and crit.chance by 1%
    public float intelligence;// 1 point increase magic damage by 1, magic resistance by 3 and MaxMana by 2
    public float vitallity;// 1 point increase health by 5 point and increase stamina by 3 point

    [Header("Offensive Staff")]
    public float Damage;
    public float MagicDamage;
    public float CritChance;
    public float CritPower;     //default value = 150%


    [Header("Defensive stats")]
    public float armor;
    public float magicArmor;
    public float evasion;

    [Header("Other stats")]
    public float movementSpeed;

    [Header("Buff")]
    public float staminaRegenRate;
    public float hpRegenRate;
    public float manaRegenRate;

    [Header("Effect")]
    public float lifesteal;
    public float canIgnite;
    public float canFreaze;
    public float canShock;
    public bool Ignite; //cause injury per second (the more attack the more time injury per second)
    public bool Freaze; // have a chance to freaze target ( freaze = stun + effect)
    public bool Shock; // Cause unable to move increase crit chance (target still able to attack)
    public bool Chill; //slowdown movementspeed have ability to become Freaze, if target is Chill and player attack with ability freaze then freaze target

    [Header("Craft Requirments")]
    public List<InventoryItem> craftingMaterials;
}
