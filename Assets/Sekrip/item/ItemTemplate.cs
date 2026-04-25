using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTemplate : MonoBehaviour
{
    public Image icon;
    public TMP_Text nameText;

    public void setup(Items item)
    {
        icon.sprite = item.sprite;
        nameText.text = item.Name;
    }
}
