using System;
using UnityEngine;

[CreateAssetMenu(menuName ="My Assets/NewScene", fileName ="New scene")]
public class ScriptableScene : ScriptableObject
{
    public enum ekspresi
    {
        normal,
        terrified,
        cautious,
        relieved
    }
    [System.Serializable]
    public struct adegan
    {
        [TextArea]
        public String Dialogue;
        public Speaker pembicara;
        public ekspresi Ekspresi;
        [Header("If Has Anim")]
        public bool hasanim;
        public Animation anim;
    }
    public adegan[] scenes;
    public Sprite Background;
    public ScriptableScene nextScene;
    
}
