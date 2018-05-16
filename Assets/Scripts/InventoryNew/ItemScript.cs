using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    public Item _Item;
    public static GameObject SelectedItem;
    public static IntVector2 SelectedItemSize;

    private float slotSize;

    public static bool isDragging = false;

    private void Awake()
    {
        slotSize = GameObject.Find("SlotGrid").GetComponent<SlotGrid>().SlotSize;
    }

    //Here we set the size and the image of the item object.
    public void SetItemObject(Item item)
    {
        RectTransform rect = GetComponent<RectTransform>();
        //No padding added yet
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, item.itemSize.x * slotSize);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, item.itemSize.y * slotSize);
        _Item = item;
        GetComponent<Image>().sprite = item.ItemIcon;
    }


    public void SetSelectedItem(GameObject obj)
    {
        SelectedItem = obj;
        SelectedItemSize = obj.GetComponent<ItemScript>()._Item.itemSize;
        isDragging = true;
        obj.GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void ResetSelectedItem()
    {
        SelectedItem = null;
        SelectedItemSize = IntVector2.Zero;
        isDragging = false;
    }
}
