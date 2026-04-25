using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//honestly its self explainatory
public class Inventory : MonoBehaviour
{
    public Items[] itemObject;
    public GameObject gridPrefab;//morelike item prefab
    public Transform grid;
    public ScrollRect scroll;

    void SetItemToInventory()
    {
        foreach (var item in itemObject)
        {
            if (!item.playerHasItem) continue; //TEMP
            GameObject obj = Instantiate(gridPrefab, grid);
            var slot = obj.GetComponent<ItemTemplate>();//ItemTemplate.cs
            slot.setup(item);
        }
    }

    IEnumerator Reset()
    {
        yield return null;
        scroll.verticalNormalizedPosition = 1f;//reset scroll pos on populate
    }

    void Start()
    {
        SetItemToInventory();
        StartCoroutine(Reset());
    }

}
