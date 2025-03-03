using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Tools;
using UnityEngine;
using Random = UnityEngine.Random;

public class ItemDatabaseManager : Singleton<ItemDatabaseManager>
{
    private Dictionary<String, GameObject> _itemDictionary = new Dictionary<string, GameObject>();
    private Dictionary<String, GameObject> _isOnlyItemDictionary = new Dictionary<string, GameObject>();
    private Dictionary<String, GameObject> _isNoneOnlyItemDictionary = new Dictionary<string, GameObject>();
    
    private void Awake()
    {
        base.Awake();
        GameObject[] gameObjects = Resources.LoadAll<GameObject>("Items");
        foreach (GameObject go in gameObjects)
        {
            ItemBase tmp_itembase = go.GetComponent<ItemBase>();
            _itemDictionary.Add(tmp_itembase.ItemData.itemName,go);
            if (tmp_itembase.ItemData.isOnly)
            {
                _isOnlyItemDictionary.Add(tmp_itembase.ItemData.itemName,go);
            }
            else
            {
                _isNoneOnlyItemDictionary.Add(tmp_itembase.ItemData.itemName,go);
            }
        }
    }

    
    public GameObject GetItemGOByName(string itemName)
    {
        return _itemDictionary[itemName];
    }

    public Dictionary<String, GameObject> GetItemDictionary()
    {
        return _itemDictionary;
    }

    public GameObject GetRandomItem()
    {
        return _itemDictionary.Values.ElementAt(Random.Range(0, _itemDictionary.Count));
    }

    public GameObject GetRandomNoneIsOnlyItem()
    {
        return _isNoneOnlyItemDictionary.Values.ElementAt(Random.Range(0, _isNoneOnlyItemDictionary.Count));
    }

    public GameObject GetRandomIsOnlyItem()
    {
        return _isOnlyItemDictionary.Values.ElementAt(Random.Range(0, _isOnlyItemDictionary.Count));
    }
    
    public List<GameObject> GetRandomNoneIsOnlyItems(int count)
    {
        List<GameObject> allItems = new List<GameObject>(_isNoneOnlyItemDictionary.Values);
        if (count >= allItems.Count)
            return allItems;
        HashSet<GameObject> selectedItems = new HashSet<GameObject>();
        while (selectedItems.Count < count)
        {
            selectedItems.Add(allItems[Random.Range(0, allItems.Count)]);
        }
        return new List<GameObject>(selectedItems);
    }
}
