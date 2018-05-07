using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class S_Weapon : ScriptableObject{
    public new string name;

    public GameObject weapon;

    public string rarity;
    public string weaponType;
    public int damage;

    [Range(0,3)]
    public int socketAmount;
    [Range(1,20)]
    public int minimumLevelRequired;
    public int maxDurability;
    public int currentDurability;
}
