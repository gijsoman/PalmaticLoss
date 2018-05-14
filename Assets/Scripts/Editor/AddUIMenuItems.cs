using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEditor;

public class AddUIMenuItems : MonoBehaviour {
    [MenuItem("GameObject/UI/Slot")]
    private static void AddSlot()
    {   
        GameObject slot = new GameObject("Slot", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Slot));
        slot.transform.parent = Selection.activeTransform;
        slot.transform.position = Selection.activeTransform.position;
    }

    [MenuItem("GameObject/UI/Slot Grid")]
    private static void AddSlotGrid()
    {

    }
}
