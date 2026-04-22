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

    [Header("Animation Curved")]
    public float JumpingDuration;
    public AnimationCurve JumpingCurved;
    public float ShakeDuration;
    public AnimationCurve ShakeCurved;

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

        //is main speaker
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

            //movde the gameobject to the location
            if (scene.scenes[index].Position == ScriptableScene.position.left)
            {
                yield return StartCoroutine(move(speakerOne, leftpoint));
            }
            else if (scene.scenes[index].Position == ScriptableScene.position.right)
            {
                yield return StartCoroutine(move(speakerOne, rightpoint));
            }
            if (scene.scenes[index].Position == ScriptableScene.position.mid)
            {
                yield return StartCoroutine(move(speakerOne, midpoint));
            }

            //anim
            if (scene.scenes[index].anim == ScriptableScene.animasi.jump)
            {
                StartCoroutine(animateJump(speakerOne));
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.shake)
            {
                StartCoroutine(animateShake(speakerOne));
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

            //movde the gameobject to the location
            if (scene.scenes[index].Position == ScriptableScene.position.left)
            {
                yield return StartCoroutine(move(speakerTwo, leftpoint));
            }
            else if (scene.scenes[index].Position == ScriptableScene.position.right)
            {
                yield return StartCoroutine(move(speakerTwo, rightpoint));
            }
            if (scene.scenes[index].Position == ScriptableScene.position.mid)
            {
                yield return StartCoroutine(move(speakerTwo, midpoint));
            }

            //anim
            if (scene.scenes[index].anim == ScriptableScene.animasi.jump)
            {
                StartCoroutine(animateJump(speakerTwo));
            }
            else if (scene.scenes[index].anim == ScriptableScene.animasi.shake)
            {
                StartCoroutine(animateShake(speakerTwo));
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
        Vector2 peakLeft = new Vector2(rect.anchoredPosition.x - 100, rect.anchoredPosition.y);
        Vector2 peakRight = new Vector2(rect.anchoredPosition.x + 100, rect.anchoredPosition.y);
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

    IEnumerator move(Image gambar, GameObject TargetPos)
    {
        RectTransform rect = gambar.GetComponent<RectTransform>();
        Vector2 startPos = rect.anchoredPosition;
        Vector2 targetPos = TargetPos.GetComponent<RectTransform>().anchoredPosition;

        float time = 0;
        float duration = 0.5f;

        Debug.Log($"Gerak dari {startPos} ke {targetPos}");
        while (time < duration)
        {
            time += Time.deltaTime;
            float t = time / duration;

            // Pakai t yang bersih (0 sampai 1)
            rect.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);

            yield return null;
        }
        rect.anchoredPosition = targetPos;
        yield return null;
    }

    void Start()
    {
        nameText.SetText(scene.scenes[0].pembicara.Name);
        GameManager.Instance.state = GameState.Running;
        runningtext = StartCoroutine(runningText(0));
    }
}
