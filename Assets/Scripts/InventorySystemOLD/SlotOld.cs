using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotOld {
    public IInventoryItem invItem;
    public bool occupied;
    public Rect slotPosition;

    public Texture2D test;

    public SlotOld(Rect _slotPosition)
    {
        slotPosition = _slotPosition;
    }

    void drawSlot()
    {
        GUI.DrawTexture(slotPosition, invItem.ItemImage);
    }
}
