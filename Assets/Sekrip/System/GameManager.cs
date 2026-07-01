using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class FlagData
{
    public string key;
    public bool value;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState state = GameState.Standby;

    public Dictionary<string, bool> FlagsChoice;
    public List<FlagData> ChoicesLock;
    public AudioSource Aud;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Debug.Log("game manager awake");
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        Debug.Log($"Instance set to {this}");

        Aud = GetComponent<AudioSource>();

        FlagsChoice = new Dictionary<string, bool>();

        foreach(var flag in ChoicesLock)
        {
            FlagsChoice[flag.key] = flag.value;
        }
    }
    
        
}
