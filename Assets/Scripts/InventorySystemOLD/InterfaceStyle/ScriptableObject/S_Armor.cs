using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Armor", menuName = "Armor")]
public class S_Armor : ScriptableObject {
    public new string name;

    public string rarity;
    public string armorType;
    public int defense;

    [Range(0, 3)]
    public int socketAmount;
    [Range(1, 20)]
    public int minimumLevelRequired;
    public int maxDurability;
    public int currentDurability;
}
