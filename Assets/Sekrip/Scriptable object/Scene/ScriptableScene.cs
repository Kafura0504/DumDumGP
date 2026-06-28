using System;
using UnityEngine;

[CreateAssetMenu(menuName ="My Assets/NewScene", fileName ="New scene")]
public class ScriptableScene : ScriptableObject
{
    public AudioClip audio;
    #region Enum
    public enum ekspresi
    {
        normal,
        terrified,
        cautious,
        relieved
    }
    public enum animasi
    {
        netral,
        jump,
        exited,
        shake,
        Entry,
        exit
        //nanti tambahin kalo ada tambahan
    }
    public enum position
    {
        left,
        right,
        mid
    }
    #endregion
    #region Struct Adegan
    [System.Serializable]
    public struct adegan
    {
        [TextArea]
        public String Dialogue; //this should be self explanatory
        public Speaker pembicara; // who's speaking?
        public ekspresi Ekspresi; //which expresion do the character will having in this spesific scenario
        public animasi anim; //plays anim. this should be self explanatory
        [Header("Positioning")]
        public bool isMainSpeaker; //is there a conversiation? if not leave this as true. it's just asking if you want to put it on speaker 1 or speaker 2. 
        public bool secondVisible; //is the second person available
        public bool mainVisible; //is main speaker visible?
        public position Position; //position of the image
    }
    #endregion
    public adegan[] scenes; //the list of scene
    public Sprite Background; //self explanatory

    [Header("Choose One")]
    [Header("If there is Next Scene")]
    public bool nextScene;
    public ScriptableScene theNextScene; //next scene
    [Header("if next is back to exploration")]
    public bool Exploration;
    public MapData mapData;
    [Header("If Next is selection Scene")]
    public bool selection;
    public SelectionContent selectionData;
}
