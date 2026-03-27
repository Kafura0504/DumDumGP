using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class BottomController : MonoBehaviour
{
    [Header("Control input")]
    public InputActionReference input;
    [Header("Scene Player")]
    public ScriptableScene scene;
    public GameState state;
    private int dialogueIndex;
    private String dialogueTextStr;
    private string displayText;
    [Header("UI Element")]
    public GameObject Bottombar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI DialogueText;
    void clicked(InputAction.CallbackContext ctx)
    {
        nameText.SetText("");
        DialogueText.SetText("");
        dialogueIndex ++;
        
    }
    public void OnEnable()
    {
        input.action.performed += clicked;
        input.action.Enable();
    }
    public void OnDisable()
    {
        input.action.performed -= clicked;
        input.action.Disable();
    }

    IEnumerator runningText(int index)
    {
        dialogueTextStr = scene.scenes[index].Dialogue;
        yield return new WaitForSeconds(0);
        for (int i = 0; i < dialogueTextStr.Length; i++)
        {
            displayText += dialogueTextStr[i];
            DialogueText.SetText(displayText);
            yield return new WaitForSeconds(0.1f);
        }
    } 
    void Start()
    {
        nameText.SetText("");
        DialogueText.SetText("");
        nameText.SetText(scene.scenes[0].pembicara.Name);
        StartCoroutine(runningText(0));
    }
}
