using UnityEngine;

public interface IInventoryItem
{
    Texture2D ItemImage { get; set; }
    int SlotX { get; set; }
    int SlotY { get; set; }
    int SlotWidth { get; set; }
    int SlotHeight { get; set; }
    string Name { get; set; }
    string Rareness { get; set; }

    void PerformAction();
}
