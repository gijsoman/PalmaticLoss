using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour, IWeapon {

    [Range(1, 1000)]
    public int damage = 0;

    public string rareness;

    private string weaponName = "Weapon";
    private int socketAmount = 0;

    public void OnPickup()
    {
       
    }

    public int Damage
    {
        set{ damage = value; }
        get{ return damage; }
    }

    public string Rareness
    {
        set{ rareness = value; }
        get{ return rareness; }
    }

    public string Name
    {
        set{ weaponName = value; }
        get{ return weaponName; }
    }

    public int SocketAmount
    {
        set{ socketAmount = value; }
        get{ return socketAmount; }
    }
}
