using UnityEngine;

public enum EquipmentType
{
    Weapon,
    Helmets,
    Chest_Armor,
    Wrap_Armor,
    Shoes,
    Gauntlets,
    Amulet,
    Ring,
    Flask
}

[CreateAssetMenu(fileName = "New Item Data", menuName = "Data/Equipment")]
public class ItemData_equipment : ItemData
{
   public EquipmentType equipmentType;
}
