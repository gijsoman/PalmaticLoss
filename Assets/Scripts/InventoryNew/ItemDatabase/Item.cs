using UnityEngine;

[System.Serializable]
public class Item
{
    public Item(string _itemName)
    {
        ItemName = _itemName;
    }
    [SerializeField]
    public string ItemName;
    public Sprite ItemIcon;
    public IntVector2 itemSize;
    [HideInInspector] public int GridPositionX;
    [HideInInspector] public int GridPositionY;
}
 