using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Selection Content",menuName ="My Assets/Selection")]
public class SelectionContent : ScriptableObject
{
    [System.Serializable]
    public struct selectionAsset
    {
        public string dialogue;
        public ScriptableScene selectedScene;
    }
    [Header("put maximum 4 array length in this")]
    public selectionAsset[] selections;
}
