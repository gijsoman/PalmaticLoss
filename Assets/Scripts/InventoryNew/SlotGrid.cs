using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotGrid : MonoBehaviour
{   
    public GameObject[,] GridSlots;
    public GameObject slotPrefab;
    public IntVector2 GridSize;
    public float SlotSize;
    public float SlotPadding;
    public float EdgePadding;

    private void Awake()
    {
        GridSlots = new GameObject[GridSize.x, GridSize.y];
        CreateSlots();
    }

    //TODO: - Make the slots scale with the parent.

    private void CreateSlots()
    {
        for (int y = 0; y < GridSize.y; y++)
        {
            for (int x = 0; x < GridSize.x; x++)
            {
                GameObject obj = Instantiate(slotPrefab, this.transform);
                RectTransform rect = obj.transform.GetComponent<RectTransform>();
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
                rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);
                rect.anchoredPosition = new Vector3(EdgePadding + x * SlotSize + SlotPadding*x, EdgePadding + y * SlotSize + SlotPadding * y, 0);
                obj.GetComponent<Slot>().GridPos = new IntVector2(x, y);
                //rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, SlotSize);
                //rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, SlotSize);
            }
        }
    }

}
