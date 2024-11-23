using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;

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

    protected StringBuilder sb = new StringBuilder();

   

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

    public virtual string GetDescription()
    {
        sb.Length = 0; // Xóa nội dung cũ trong StringBuilder

        // Major Stats
        if (strength > 0) AddItemDescription(+ strength, "Strength");
        if (agility > 0) AddItemDescription(agility, "Agility");
        if (intelligence > 0) AddItemDescription(intelligence, "Intelligence");
        if (vitallity > 0) AddItemDescription(vitallity, "Vitality");

        // Offensive Stats
        if (Damage > 0) AddItemDescription(Damage, "Damage");
        if (MagicDamage > 0) AddItemDescription(MagicDamage, "Magic Damage");
        if (CritChance > 0) AddItemDescription(CritChance, "Critical Chance");
        if (CritPower > 0) AddItemDescription(CritPower, "Critical Power");

        // Defensive Stats
        if (armor > 0) AddItemDescription(armor, "Armor");
        if (magicArmor > 0) AddItemDescription(magicArmor, "Magic Armor");
        if (evasion > 0) AddItemDescription(evasion, "Evasion");

        // Other Stats
        if (movementSpeed > 0) AddItemDescription(movementSpeed, "Movement Speed");

        // Buffs
        if (staminaRegenRate > 0) AddItemDescription(staminaRegenRate, "Stamina Regen Rate");
        if (hpRegenRate > 0) AddItemDescription(hpRegenRate, "HP Regen Rate");
        if (manaRegenRate > 0) AddItemDescription(manaRegenRate, "Mana Regen Rate");

        // Effects
        if (lifesteal > 0) AddItemDescription(lifesteal, "Lifesteal");
        if (canIgnite > 0) AddItemDescription(canIgnite, "Can Ignite");
        if (canFreaze > 0) AddItemDescription(canFreaze, "Can Freeze");
        if (canShock > 0) AddItemDescription(canShock, "Can Shock");

      

        return sb.ToString(); // Trả về chuỗi mô tả cuối cùng
    }


    private void AddItemDescription(float _value, string _name)
    {
        if (_value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (_value > 0)
                sb.AppendLine("+ " + _value + " " + _name);

        }
    }
}
