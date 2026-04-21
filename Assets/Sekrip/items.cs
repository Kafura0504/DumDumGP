using System;
using UnityEngine;

[CreateAssetMenu(menuName ="My Assets/Item", fileName ="An Iten")]
public class items : ScriptableObject
{
    [System.Serializable]
    public struct itemsClass
    {
        public String Name;
        [TextArea]
        public String Description;
        public bool playerHasItem;
        public Sprite sprite;
    }
    public itemsClass[] itemObject;
}