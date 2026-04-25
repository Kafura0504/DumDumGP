using System;
using UnityEngine;

[CreateAssetMenu(menuName ="My Assets/Item", fileName ="An Iten")]
public class Items : ScriptableObject
{
        public String Name;
        [TextArea]
        public String Description;
        public bool playerHasItem;
        public Sprite sprite;
}