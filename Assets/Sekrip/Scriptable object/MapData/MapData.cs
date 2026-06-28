using System;
using UnityEngine;

[CreateAssetMenu(menuName ="My Assets/Mapdata", fileName ="NewMapData")]
public class MapData : ScriptableObject
{
    public enum ekspresi
    {
        normal,
        terrified,
        cautious,
        relieved
    }

    public enum position
    {
        left,
        right,
        mid
    }

    public Sprite Background;
    [Header("First NPC Setting")]
    public bool NpcOne;
    public Speaker speakerOne;
    public ekspresi speakerOneExpression;
    public position speakerOnePosition;
    [Header("Second NPC setting")]
    public bool NpcTwo;
    public Speaker speakerTwo;
    public ekspresi speakerTwoExpression;
    public position speakerTwoPosition;
    public AudioClip BGM;
}
