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
    [Range(0.1f, 0.01f)]
    public float textspeed;
    [Header("Scene Player")]
    public ScriptableScene scene;
    private int dialogueIndex;
    private String dialogueTextStr;
    private string displayText;
    private Coroutine runningtext;
    [Header("UI Element")]
    public GameObject selectionBar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI DialogueText;
    public Image speakerOne;
    public Image speakerTwo;
    public Image Background;
    [Header("position refference")]
    public GameObject leftpoint;
    public GameObject midpoint;
    public GameObject rightpoint;

    [Header("Animation Curved")]
    public float JumpingDuration;
    public AnimationCurve JumpingCurved;
    public float ShakeDuration;
    public AnimationCurve ShakeCurved;
    
    //Private
    private LoadMapData loader;
    private bool canClick = true;
    private SelectionSystem[] Selections;
    public static event Action done;

    void clicked(InputAction.CallbackContext ctx)
    {

        if (!canClick)
        return;

        StartCoroutine(InputCooldown());
        //this will run when you press Mouse btn 1
        if (GameManager.Instance.state == GameState.Standby)
        {
            dialogueIndex++; //add index
            DialogueText.SetText(""); //reset text dialogue
            if (dialogueIndex >= scene.scenes.Length)
            {
                dialogueIndex = 0;
                DialogueText.SetText("");

                if (scene.Exploration)
                {
                    loader.data = scene.mapData;
                    loader.scene = null;
                    loader.enabled = true;
                    gameObject.SetActive(false);
                    done?.Invoke();
                }
                else if (scene.nextScene)
                {
                    scene = scene.theNextScene;
                    nameText.SetText(scene.scenes[0].pembicara.Name);
                    Background.sprite = scene.Background;
                    GameManager.Instance.state = GameState.Running;
                    runningtext = StartCoroutine(runningText(0));
                }
                else if(scene.selection)
                {
                    GameManager.Instance.state = GameState.Choosing;
                    for (int i = 0; i < scene.selectionData.selections.Length; i++)
                    {
                        if (!scene.selectionData.selections[i].isLocked || GameManager.Instance.FlagsChoice[scene.selectionData.selections[i].lockName])
                        {    
                        Selections[i].scene = scene.selectionData.selections[i].selectedScene;
                        Selections[i].dialogue = scene.selectionData.selections[i].dialogue;
                        if (scene.selectionData.selections[i].isUnlock)
                        {
                            Selections[i].unlockFlag = scene.selectionData.selections[i].keyName;
                        }
                        Selections[i].gameObject.SetActive(true);
                        }
                    }
                    selectionBar.SetActive(true);
                    canClick = true;
                    gameObject.SetActive(false);
                    return;
                }
                return;
            }
            runningtext = StartCoroutine(runningText(dialogueIndex)); //start coroutine
        }
        else if (GameManager.Instance.state == GameState.Running)
        {
            StopCoroutine(runningtext); //stop running coroutine
            DialogueText.SetText(dialogueTextStr); // skip the running dialogue
            displayText = ""; //reset variable
            GameManager.Instance.state = GameState.Standby; //reset game state
        }

        
    }
    public void OnEnable()
    {
        input.action.started += clicked;
        input.action.Enable();
        //start the running text
        nameText.SetText(scene.scenes[0].pembicara.Name);
        Background.sprite = scene.Background;
        GameManager.Instance.state = GameState.Running;
        runningtext = StartCoroutine(runningText(0));
    }
    public void OnDisable()
    {
        input.action.started -= clicked;
        input.action.Disable();
    }
    IEnumerator InputCooldown()
    {
        canClick = false;
        yield return new WaitForSeconds(0.25f);
        canClick = true;
    }

    void setInvis(GameObject speaker)
    {
        speaker.SetActive(false);
    }

    void setVisible(GameObject speaker)
    {
        speaker.SetActive(true);
    }

    void setcolor(Image TheSpeaking, Image TheSilent)
    {
        TheSpeaking.color = new Color(1f, 1f, 1f);
        TheSilent.color =new Color(0.5f, 0.5f, 0.5f);
    }

    void setExpression(Image speaker, int index)
    {
        if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.normal)
            {
                speaker.sprite = scene.scenes[index].pembicara.ekspresi.Normal; //makes the speaker look normal in her/his normal expression
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.terrified)
            {
                speaker.sprite = scene.scenes[index].pembicara.ekspresi.terrified; //make the speaker terrified
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.relieved)
            {
                speaker.sprite = scene.scenes[index].pembicara.ekspresi.relieved; //make the speaker relieved
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.cautious)
            {
                speaker.sprite = scene.scenes[index].pembicara.ekspresi.Cautious; // make the speaker cautious
            }
    }

    IEnumerator animateJump(Image target)
    {
        yield return null;
        //saving original and desired Position
        RectTransform rect = target.gameObject.GetComponent<RectTransform>();
        Vector2 original = rect.anchoredPosition;
        Vector2 peak = new Vector2(rect.anchoredPosition.x, rect.anchoredPosition.y + 100);
        float time = 0;
        while (time < JumpingDuration)
        {
            //setting up curve
            time += Time.deltaTime;
            float normalizedTime = time / JumpingDuration;
            float curveValue = JumpingCurved.Evaluate(normalizedTime);

            rect.anchoredPosition = Vector2.Lerp(original, peak, curveValue);
            yield return null;
        }
        rect.anchoredPosition = original;
    }
    IEnumerator animateShake(Image target)
    {
        yield return null;
        //saving original and desired Position
        RectTransform rect = target.gameObject.GetComponent<RectTransform>();
        Vector2 original = rect.anchoredPosition;
        float time = 0;

        while (time < ShakeDuration)
        {
            //setting up curve
            time += Time.deltaTime;
            float normalizedTime = time / ShakeDuration;
            float curveValue = JumpingCurved.Evaluate(normalizedTime);

            float offset = Mathf.Lerp(-50f, 50f, curveValue);
            rect.anchoredPosition = new Vector2(original.x + offset, original.y);
            yield return null;
        }
        //lerp back into the position
        float returnTime = 0;
        float returnDuration = 0.2f;
        Vector2 currentPos = rect.anchoredPosition;
        while (returnTime < returnDuration)
        {
            returnTime += Time.deltaTime;
            float t = returnTime / returnDuration;
            rect.anchoredPosition = Vector2.Lerp(currentPos, original, t);
            yield return null;
        }
    }

    void move(Image gambar, GameObject TargetPos)
    {
        RectTransform rect = gambar.GetComponent<RectTransform>();
        RectTransform targetPos = TargetPos.GetComponent<RectTransform>();

        rect.anchoredPosition = targetPos.anchoredPosition;
    }

    void MoveSpeakerToPosition(Image speaker, ScriptableScene.position position)
    {
        switch (position)
        {
            case ScriptableScene.position.left:
                move(speaker, leftpoint);
                break;

            case ScriptableScene.position.mid:
                move(speaker, midpoint);
                break;

            case ScriptableScene.position.right:
                move(speaker, rightpoint);
                break;
        }
    }

    void playAnimation(Image speaker, ScriptableScene.animasi anim)
    {
        if (anim == ScriptableScene.animasi.jump)
            {
                StartCoroutine(animateJump(speaker));
            }
            else if (anim == ScriptableScene.animasi.shake)
            {
                StartCoroutine(animateShake(speaker));
            }
    }

    void visibilityChange(int index)
    {
        if (scene.scenes[index].secondVisible)
        {
            setVisible(speakerTwo.gameObject);
        }
        else if (!scene.scenes[index].secondVisible)
        {
            setInvis(speakerTwo.gameObject);
        }
        if (scene.scenes[index].mainVisible)
        {
            setVisible(speakerOne.gameObject);
        }
        else if (!scene.scenes[index].mainVisible)
        {
            setInvis(speakerOne.gameObject);
        }
    }

    //THE WHOLE THING ARE HERE
    IEnumerator runningText(int index)
    {
        //set visibility
        visibilityChange(index);

        //setting up Stuff
        GameManager.Instance.state = GameState.Running; //make the game know the text still running
        displayText = ""; //reseting display text
        dialogueTextStr = scene.scenes[index].Dialogue; //setting up placeholder variable
        nameText.SetText(scene.scenes[index].pembicara.Name); //setting up name

        //is main speaker
        if (scene.scenes[index].isMainSpeaker)
        {
            setcolor(speakerOne,speakerTwo);

            //settin image based on expression
            setExpression(speakerOne,index);

            //movde the gameobject to the location
            MoveSpeakerToPosition(speakerOne, scene.scenes[index].Position);

            //anim
            playAnimation(speakerOne,scene.scenes[index].anim);
        }

        //kalo bukan main speaker
        else if (!scene.scenes[index].isMainSpeaker)
        {
            //set positioning
            setcolor(speakerTwo,speakerOne);

            //settin image based on expression
            setExpression(speakerTwo,index);

            //movde the gameobject to the location
            MoveSpeakerToPosition(speakerTwo, scene.scenes[index].Position);

            //anim
            playAnimation(speakerTwo,scene.scenes[index].anim);
            
        }
            //running text loop
            for (int i = 0; i < dialogueTextStr.Length; i++)
            {
                displayText += dialogueTextStr[i];
                DialogueText.SetText(displayText);
                yield return new WaitForSeconds(textspeed);
            }
            displayText = "";
            GameManager.Instance.state = GameState.Standby;
    }

    void Start()
    {
        loader = GetComponentInParent<LoadMapData>();
        Selections = selectionBar.GetComponentsInChildren<SelectionSystem>(true);
    }

}