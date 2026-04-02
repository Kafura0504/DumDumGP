using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class BottomController : MonoBehaviour
{
    [Header("Control input")]
    public InputActionReference input;
    [Header("Scene Player")]
    public ScriptableScene scene;
    public GameState state = GameState.Standby;
    private int dialogueIndex;
    private String dialogueTextStr;
    private string displayText;
    private Coroutine runningtext;
    [Header("UI Element")]
    public GameObject Bottombar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI DialogueText;
    public Image img;
    void clicked(InputAction.CallbackContext ctx)
    {
        if (state == GameState.Standby)
        {
        DialogueText.SetText("");
        dialogueIndex ++;
        Debug.Log(dialogueIndex);
        runningtext = StartCoroutine(runningText(dialogueIndex));
        Debug.Log(runningtext);
        }
        else if (state == GameState.Running)
        {
            StopCoroutine(runningtext);
            DialogueText.SetText(dialogueTextStr);
            displayText="";
            state = GameState.Standby;
        }
    }
    public void OnEnable()
    {
        input.action.started += clicked;
        input.action.Enable();
    }
    public void OnDisable()
    {
        input.action.started -= clicked;
        input.action.Disable();
    }

    IEnumerator runningText(int index)
    {
        //setting up Stuff
        state = GameState.Running;
        displayText = "";
        dialogueTextStr = scene.scenes[index].Dialogue;
        nameText.SetText(scene.scenes[index].pembicara.Name);
        
        //settin image based on expression
        if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.normal)
        {
            img.sprite = scene.scenes[index].pembicara.ekspresi.Normal;
        }
        else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.terrified)
        {
            img.sprite = scene.scenes[index].pembicara.ekspresi.terrified;
        }
        else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.relieved)
        {
            img.sprite = scene.scenes[index].pembicara.ekspresi.relieved;
        }
        else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.cautious)
        {
            img.sprite = scene.scenes[index].pembicara.ekspresi.Cautious;
        }
        
        //running text loop
        for (int i = 0; i < dialogueTextStr.Length; i++)
        {
            displayText += dialogueTextStr[i];
            DialogueText.SetText(displayText);
            yield return new WaitForSeconds(0.1f);
        }
        displayText = "";
        state = GameState.Standby;
    } 
    void Start()
    {
        nameText.SetText(scene.scenes[0].pembicara.Name);
        runningtext = StartCoroutine(runningText(0));
        state = GameState.Running;
    }
}
