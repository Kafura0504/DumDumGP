using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ItemDB : MonoBehaviour
{
    public List<Items> itemsList;
    private Dictionary<int, Items> itemLookup;
    public static ItemDB Instance;

#if UNITY_EDITOR
    [ContextMenu("Load items")]
    void loadItems()
    {
        itemsList.Clear();
        string[] itemGUID = AssetDatabase.FindAssets("t:Items", new[] { "Assets/Sekrip/Scriptable object/items" });
        foreach (string guid in itemGUID)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Items items = AssetDatabase.LoadAssetAtPath<Items>(path);
            itemsList.Add(items);
        }
    }
#endif
    void Awake()
    {
        itemLookup = new Dictionary<int, Items>();
        foreach (Items item in itemsList)
        {
            itemLookup[item.id] = item;
        }

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    public Items GetItems(int id)
    {
        if (itemLookup.TryGetValue(id, out Items items))
        {
            return items;
        }
        return null;
    }

}
