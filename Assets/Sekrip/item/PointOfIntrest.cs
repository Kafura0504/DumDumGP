using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PointOfIntrest : MonoBehaviour, IPointerClickHandler
{
    public Items itemData;
    [SerializeField] private Image icon;
    public bool deleteAftrClk;


    public void OnPointerClick(PointerEventData eventData)
    {
        Inventory.Instance.Insert(ItemDB.Instance.GetItems(itemData.id));//stupidly overcomplicated, it can be literally itemdata
        if (deleteAftrClk)
            Destroy(this.gameObject);
    }
    public void Setup(Items item)
    {
        if (item == null) return;
        itemData = item;
        icon.sprite = itemData.sprite;
    }
    private void OnValidate()
    {
        Setup(itemData);
    }
    private void Start()
    {
        Setup(itemData);
    }
}
