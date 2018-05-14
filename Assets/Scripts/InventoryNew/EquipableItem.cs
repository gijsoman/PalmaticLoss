using UnityEngine;

public enum EquipmentType
{
    Helm,
    Chest,
    Hands,
    Waist,
    Legs,
    Boots,
}

[CreateAssetMenu]
public class EquipableItem : Item {
    public EquipmentType EquipmentType;
}
