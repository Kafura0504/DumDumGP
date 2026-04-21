using System;
using UnityEngine;
[CreateAssetMenu(menuName = "My Assets/Speaker", fileName ="New Speaker")]
public class Speaker : ScriptableObject
{
    [System.Serializable]
    public struct Expresion
    {
        public Sprite Normal;
        public Sprite terrified;
        public Sprite Cautious;
        public Sprite relieved;
    }
    public String Name;
    public Color color;
    public Expresion ekspresi;
    
}
