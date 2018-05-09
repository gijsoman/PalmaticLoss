using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot {
    public IInventoryItem invItem;
    public bool occupied;
    public Rect slotPosition;

    public Texture2D test;

    public Slot(Rect _slotPosition)
    {
        slotPosition = _slotPosition;
    }

    void drawSlot()
    {
        GUI.DrawTexture(slotPosition, invItem.ItemImage);
    }
}
