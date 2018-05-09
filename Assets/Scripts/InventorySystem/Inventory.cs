using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
    public Texture2D image;
    public Rect menuPosition;

    public List<IInventoryItem> items = new List<IInventoryItem>();
    int amountOfSlotsWidth = 4;
    int amountOfSlotsHeight = 10;
    public Slot[,] slots;

    public int firstSlotX;
    public int firstSlotY;

    public int slotWidth;
    public int slotHeight;

    private void Start()
    {
        setSlots();
    }

    private void OnGUI()
    {
        drawInventory();
    }

    void setSlots()
    {
        slots = new Slot[amountOfSlotsWidth, amountOfSlotsHeight];
        for (int x = 0; x < amountOfSlotsWidth; x++)
        {
            for (int y = 0; y < amountOfSlotsHeight; y++)
            {

            }
        }
    }

    private void drawInventory()
    {
        menuPosition.x = Screen.width - menuPosition.width;
        menuPosition.y = Screen.height - menuPosition.height - Screen.height * 0.2f;
        GUI.DrawTexture(menuPosition, image);
    }
}
