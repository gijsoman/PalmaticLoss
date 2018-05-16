using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemDatabase : ScriptableObject
{
    [SerializeField]
    private List<Item> itemDatabase;

    private void OnEnable()
    {
        if (itemDatabase == null)
        {
            itemDatabase = new List<Item>();
        }
    }

    public void Add(Item item)
    {
        itemDatabase.Add(item);
    }

    public void Remove(Item item)
    {
        itemDatabase.Remove(item);
    }

    public void RemoveAt(int index)
    {
        itemDatabase.RemoveAt(index);
    }

    public int COUNT
    {
        get { return itemDatabase.Count; }
    }

    public Item ItemAt(int index)
    {
        return itemDatabase.ElementAt(index);
    }

    public void SortAlphabeticallyAtoZ()
    {
        itemDatabase.Sort((x, y) => string.Compare(x.ItemName, y.ItemName));
    }
}
