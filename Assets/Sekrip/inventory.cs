using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class inventory : MonoBehaviour
{
    public items itemObject;
    public GameObject gridPrefab;
    public Transform grid;
    public ScrollRect scroll;

    void SetItemToInventory()
    {
        foreach (var item in itemObject.itemObject)
        {
            if (!item.playerHasItem) continue;
            GameObject obj = Instantiate(gridPrefab, grid);
            var slot = obj.GetComponent<ItemTemplate>();
            slot.setup(item);
        }
    }

    IEnumerator Reset()
    {
        yield return null;
        scroll.verticalNormalizedPosition = 1f;
    }

    void Start()
    {
        SetItemToInventory();
        StartCoroutine(Reset());
    }

}
