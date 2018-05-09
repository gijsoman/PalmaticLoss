using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Weapon : MonoBehaviour {

    [Range(1, 1000)]
    public int damage = 0;

    public string rareness;

    private string weaponName = "Weapon";
    private int socketAmount = 0;

    Texture2D itemImage;

    public void OnPickup()
    {
       
    }

    Texture2D ItemImage
    {
        set { itemImage = value; }
        get { return itemImage; }
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
