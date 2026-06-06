using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class Inventory : MonoBehaviour
{
    public static Inventory Instance;
    public List<Items> itemObject = null;
    public event Action OnChange;

    public void Insert(Items item)
    {
        if (!itemObject.Contains(item))
            itemObject.Add(item);
        OnChange?.Invoke();
    }
    public void Remove(Items item)
    {
        if (itemObject.Contains(item))
            itemObject.Remove(item);
        OnChange?.Invoke();
    }

    void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}