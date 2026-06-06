using System.Collections;
using UnityEngine;
using UnityEngine.UI;
//honestly its self explainatory
public class InventoryUI : MonoBehaviour
{
    public GameObject ItemPrefab;
    public Transform grid;
    public ScrollRect scroll;

    public void SetItemToInventory()
    {
        foreach (Transform child in grid)
        {
            Destroy(child.gameObject); //clear all
        }
        {
            foreach (Items item in Inventory.Instance.itemObject)
            {
                GameObject obj = Instantiate(ItemPrefab, grid);
                var slot = obj.GetComponent<ItemTemplate>();//ItemTemplate.cs
                slot.Setup(item);
            }
        }
    }

    IEnumerator Reset()
    {
        yield return null;
        scroll.verticalNormalizedPosition = 1f;//reset scroll pos on populate
    }

    void Start()
    {
        Inventory.Instance.OnChange += SetItemToInventory;
        SetItemToInventory();
        StartCoroutine(Reset());
    }

    void OnDestroy()
    {
        Inventory.Instance.OnChange -= SetItemToInventory;
    }
}