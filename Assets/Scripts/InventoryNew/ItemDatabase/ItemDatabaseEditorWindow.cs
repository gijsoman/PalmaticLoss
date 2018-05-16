using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class ItemDatabaseEditorWindow : EditorWindow
{
    private enum State
    {
        BLANK,
        EDIT,
        ADD
    }

    private State state;
    private int selectedItem;

    private string newItemName;

    private const string DATABASE_PATH = @"Assets/Database/ItemDatabase.asset";

    private ItemDatabase itemDatabase;
    private Vector2 scrollPosition;

    [MenuItem("CustomWindows/Database/Item Database %#w")]
    public static void Initialize()
    {
        ItemDatabaseEditorWindow window = EditorWindow.GetWindow<ItemDatabaseEditorWindow>();
        window.minSize = new Vector2(800, 400);
        window.Show();
    }

    private void OnEnable()
    {
        if (itemDatabase == null)
        {
            LoadDatabase();
        }

        state = State.BLANK;
    }

    private void OnGUI()
    {
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        DisplayListArea();
        DisplayMainArea();
        EditorGUILayout.EndHorizontal();
    }

    private void LoadDatabase()
    {
        itemDatabase = (ItemDatabase)AssetDatabase.LoadAssetAtPath(DATABASE_PATH, typeof(ItemDatabase));
        if (itemDatabase == null)
        {
            CreateDatabase();
        }
    }

    private void CreateDatabase()
    {
        itemDatabase = CreateInstance<ItemDatabase>();
        AssetDatabase.CreateAsset(itemDatabase, DATABASE_PATH);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void DisplayListArea()
    {
        EditorGUILayout.BeginVertical(GUILayout.Width(250));
        EditorGUILayout.Space();

        scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition, "box", GUILayout.ExpandHeight(true));

        for (int i = 0; i < itemDatabase.COUNT; i++)
        {
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                itemDatabase.RemoveAt(i);
                itemDatabase.SortAlphabeticallyAtoZ();
                EditorUtility.SetDirty(itemDatabase);
                state = State.BLANK;
                return;
            }

            if (GUILayout.Button(itemDatabase.ItemAt(i).ItemName, "box", GUILayout.ExpandWidth(true)))
            {
                selectedItem = i;
                state = State.EDIT;
            }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUILayout.EndScrollView();
        EditorGUILayout.BeginHorizontal(GUILayout.ExpandWidth(true));
        EditorGUILayout.LabelField("Items: " + itemDatabase.COUNT, GUILayout.Width(100));

        if (GUILayout.Button("New Item"))
        {
            state = State.ADD;
        }

        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        EditorGUILayout.EndVertical();
    }

    private void DisplayMainArea()
    {
        EditorGUILayout.BeginVertical(GUILayout.ExpandWidth(true));
        EditorGUILayout.Space();

        switch (state)
        {
            case State.ADD:
                DisplayAddMainArea();
                break;
            case State.EDIT:
                DisplayEditMainArea();
                break;
            case State.BLANK:
                DisplayBlankMainArea();
                break;
        }

        EditorGUILayout.EndVertical();
    }

    private void DisplayBlankMainArea()
    {
        EditorGUILayout.LabelField(
            "This is the main field were the following will be shown. \n" +
            "1) Item info for editing. \n" +
            "2) Black fields for adding a new item. \n" +
            "3) Blank Area",
            GUILayout.ExpandHeight(true));
    }

    private void DisplayEditMainArea()
    {
        itemDatabase.ItemAt(selectedItem).ItemName = EditorGUILayout.TextField(new GUIContent("Item Name: "), itemDatabase.ItemAt(selectedItem).ItemName);
        EditorGUILayout.Space();

        if (GUILayout.Button("Done", GUILayout.Width(100)))
        {
            itemDatabase.SortAlphabeticallyAtoZ();
            EditorUtility.SetDirty(itemDatabase);
            state = State.BLANK;
        }
    }

    private void DisplayAddMainArea()
    {
        newItemName = EditorGUILayout.TextField(new GUIContent("Name: "), newItemName);

        EditorGUILayout.Space();

        if (GUILayout.Button("Done", GUILayout.Width(100)))
        {
            itemDatabase.Add(new Item(newItemName));
            itemDatabase.SortAlphabeticallyAtoZ();

            newItemName = string.Empty;

            EditorUtility.SetDirty(itemDatabase);
            state = State.BLANK;
        }
    }
}

