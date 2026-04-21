using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
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
    public GameObject Bottombar;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI DialogueText;
    public Image speakerOne;
    public Image speakerTwo;
    [Header("position refference")]
    public GameObject leftpoint;
    public GameObject midpoint;
    public GameObject rightpoint;

    //Private
    private Animator animationOne;
    private Animator animationTwo;

    void clicked(InputAction.CallbackContext ctx)
    {
        //this will run when you press Mouse btn 1
        if (GameManager.Instance.state == GameState.Standby)
        {
            DialogueText.SetText(""); //reset text dialogue
            dialogueIndex++; //add index
            Debug.Log(dialogueIndex); //just siwwy wittle debug
            runningtext = StartCoroutine(runningText(dialogueIndex)); //start coroutine
            Debug.Log(runningtext);
        }
        else if (GameManager.Instance.state == GameState.Running)
        {
            StopCoroutine(runningtext); //stop running coroutine
            DialogueText.SetText(dialogueTextStr); // skip the running dialogue
            displayText = ""; //reset variable
            GameManager.Instance.state = GameState.Standby; //reset game state

            //reset animation2
            animationTwo.ResetTrigger("Shake");
            animationTwo.ResetTrigger("Jump");
            animationTwo.ResetTrigger("Exited");
            animationTwo.ResetTrigger("Out");
            //reset animation1
            animationOne.ResetTrigger("Entry");
            animationOne.ResetTrigger("Shake");
            animationOne.ResetTrigger("Jump");
            animationOne.ResetTrigger("Exited");
            animationOne.ResetTrigger("Out");
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
        //set visibility
        if (scene.scenes[index].secondVisible)
        {
            speakerTwo.gameObject.SetActive(true);
        }
        else if (!scene.scenes[index].secondVisible)
        {
            speakerTwo.gameObject.SetActive(false);
        }
        if (scene.scenes[index].mainVisible)
        {
            speakerOne.gameObject.SetActive(true);
        }
        else if (!scene.scenes[index].mainVisible)
        {
            speakerOne.gameObject.SetActive(false);
        }
        if (scene.scenes[index].isMainSpeaker)
        {
            //set positioning
            if (scene.scenes[index].secondVisible)
            {
                speakerTwo.gameObject.SetActive(true); //set active the one who enter the convo
            }
            else if (!scene.scenes[index].secondVisible)
            {
                speakerTwo.gameObject.SetActive(false); //set deactive the one who left the convo
            }
            speakerTwo.color = new Color(0.5f, 0.5f, 0.5f); //delighting the one who doesn't talk
            speakerOne.color = new Color(1f, 1f, 1f); //highlighting the one talking

            //setting up Stuff
            GameManager.Instance.state = GameState.Running; //make the game know the text still running
            displayText = ""; //reseting display text
            dialogueTextStr = scene.scenes[index].Dialogue; //setting up placeholder variable
            nameText.SetText(scene.scenes[index].pembicara.Name); //setting up name

            //movde the gameobject to the location
            // if (scene.scenes[index].Position == ScriptableScene.position.left)
            // {
            //     move(speakerOne, leftpoint);
            // }
            // else if (scene.scenes[index].Position == ScriptableScene.position.right)
            // {
            //     move(speakerOne, rightpoint);
            // }
            // if (scene.scenes[index].Position == ScriptableScene.position.mid)
            // {
            //     move(speakerOne, midpoint);
            // }

            //anim
            if (scene.scenes[index].anim == ScriptableScene.animasi.netral)
            {
                //this all just force the anim to the Static state
                animationOne.ResetTrigger("Entry");
                animationOne.ResetTrigger("Shake");
                animationOne.ResetTrigger("Jump");
                animationOne.ResetTrigger("Exited");
                animationOne.ResetTrigger("Out");
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.Entry)
            {
                animationOne.SetTrigger("Entry"); //make the anim to fade in
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.exit)
            {
                animationOne.SetTrigger("Out"); //make the anim to fade out
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.exited)
            {
                animationOne.SetTrigger("Exited"); //make the anim to exited
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.jump)
            {
                animationOne.SetTrigger("Jump"); //make the anim to Jump
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.shake)
            {
                animationOne.SetTrigger("Shake"); //make the anim to Shake 
            }

            //settin image based on expression
            if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.normal)
            {
                speakerOne.sprite = scene.scenes[index].pembicara.ekspresi.Normal; //makes the speaker look normal in her/his normal expression
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.terrified)
            {
                speakerOne.sprite = scene.scenes[index].pembicara.ekspresi.terrified; //make the speaker terrified
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.relieved)
            {
                speakerOne.sprite = scene.scenes[index].pembicara.ekspresi.relieved; //make the speaker relieved
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.cautious)
            {
                speakerOne.sprite = scene.scenes[index].pembicara.ekspresi.Cautious; // make the speaker cautious
            }

            //running text loop
            for (int i = 0; i < dialogueTextStr.Length; i++)
            {
                displayText += dialogueTextStr[i];
                DialogueText.SetText(displayText);
                yield return new WaitForSeconds(textspeed);
            }
            //reset everything
            displayText = "";
            GameManager.Instance.state = GameState.Standby;
            animationOne.ResetTrigger("Entry");
            animationOne.ResetTrigger("Shake");
            animationOne.ResetTrigger("Jump");
            animationOne.ResetTrigger("Exited");
            animationOne.ResetTrigger("Out");
        }

        //kalo bukan main speaker
        else if (!scene.scenes[index].isMainSpeaker)
        {
            //set positioning
            if (scene.scenes[index].secondVisible)
            {
                speakerTwo.gameObject.SetActive(true);
            }
            else if (!scene.scenes[index].secondVisible)
            {
                speakerTwo.gameObject.SetActive(false);
            }
            speakerOne.color = new Color(0.5f, 0.5f, 0.5f);
            speakerTwo.color = new Color(1f, 1f, 1f);

            //setting up Stuff
            GameManager.Instance.state = GameState.Running; //make the game know the text still running
            displayText = ""; //reseting display text
            dialogueTextStr = scene.scenes[index].Dialogue; //setting up placeholder variable
            nameText.SetText(scene.scenes[index].pembicara.Name); //setting up name

            //movde the gameobject to the location
            // if (scene.scenes[index].Position == ScriptableScene.position.left)
            // {
            //     move(speakerTwo, leftpoint);
            // }
            // else if (scene.scenes[index].Position == ScriptableScene.position.right)
            // {
            //     move(speakerTwo, rightpoint);
            // }
            // if (scene.scenes[index].Position == ScriptableScene.position.mid)
            // {
            //     move(speakerTwo, midpoint);
            // }

            //anim
            if (scene.scenes[index].anim == ScriptableScene.animasi.netral)
            {
                //this all just force the anim to the Static state
                animationTwo.ResetTrigger("Entry");
                animationTwo.ResetTrigger("Shake");
                animationTwo.ResetTrigger("Jump");
                animationTwo.ResetTrigger("Exited");
                animationTwo.ResetTrigger("Out");
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.Entry)
            {
                animationTwo.SetTrigger("Entry"); //make the anim to fade in
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.exit)
            {
                animationTwo.SetTrigger("Out"); //make the anim to fade out
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.exited)
            {
                animationTwo.SetTrigger("Exited"); //make the anim to exited
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.jump)
            {
                animationTwo.SetTrigger("Jump"); //make the anim to Jump
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.shake)
            {
                animationTwo.SetTrigger("Shake"); //make the anim to Shake 
            }

            //settin image based on expression
            if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.normal)
            {
                speakerTwo.sprite = scene.scenes[index].pembicara.ekspresi.Normal; //makes the speaker look normal in her/his normal expression
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.terrified)
            {
                speakerTwo.sprite = scene.scenes[index].pembicara.ekspresi.terrified; //make the speaker terrified
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.relieved)
            {
                speakerTwo.sprite = scene.scenes[index].pembicara.ekspresi.relieved; //make the speaker relieved
            }
            else if (scene.scenes[index].Ekspresi == ScriptableScene.ekspresi.cautious)
            {
                speakerTwo.sprite = scene.scenes[index].pembicara.ekspresi.Cautious; // make the speaker cautious
            }

            //running text loop
            for (int i = 0; i < dialogueTextStr.Length; i++)
            {
                displayText += dialogueTextStr[i];
                DialogueText.SetText(displayText);
                yield return new WaitForSeconds(0.01f);
            }
            displayText = "";
            GameManager.Instance.state = GameState.Standby;
            animationTwo.ResetTrigger("Entry");
            animationTwo.ResetTrigger("Shake");
            animationTwo.ResetTrigger("Jump");
            animationTwo.ResetTrigger("Exited");
            animationTwo.ResetTrigger("Out");
        }
    }

    // void move(Image gambar,GameObject TargetPos)
    // {
    //     RectTransform rect = gambar.GetComponent<RectTransform>();
    //         Vector2 target = TargetPos.GetComponent<RectTransform>().anchoredPosition;

    // while (Vector2.Distance(rect.anchoredPosition, target) > 0.1f)
    // {
    //     Vector2 currentPos = Vector2.Lerp(rect.anchoredPosition, target, 0.5f);
    //     rect.anchoredPosition = currentPos;
    // }
    // rect.anchoredPosition = target;
    // }
    void Start()
    {      
        nameText.SetText(scene.scenes[0].pembicara.Name);
        GameManager.Instance.state = GameState.Running;
        animationOne = speakerOne.GetComponent<Animator>();
        animationTwo = speakerTwo.GetComponent<Animator>();
        runningtext = StartCoroutine(runningText(0));
    }
}
