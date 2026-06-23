using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour
{
    [Tooltip("The bottom bar controller on the BottomBar")]
    public BottomController controller;
    [Tooltip("The Scene that will be played when the player interact")]
    public ScriptableScene scene;
    
    //Private
    private Image sprite;
    private LoadMapData loaderMap;
    void Start()
    {
        sprite = GetComponent<Image>();
        loaderMap = GetComponentInParent<LoadMapData>();
    }

    public void interactNPC()
    {
        Debug.Log("Line Of code ini terbaca");
        StartCoroutine(changeToDialogue());
    }
    IEnumerator changeToDialogue()
    {
        //setting played scene
        controller.scene = scene;
        //fade out NPC
        float duration = 1f;
        float elapsed = 0f;

        Color start = sprite.color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            Color c = start;
            c.a = Mathf.Lerp(1f, 0f, elapsed / duration);

            sprite.color = c;

            yield return null;
        }
        //Play the Bottombar Controller
        controller.gameObject.SetActive(true);
        gameObject.SetActive(false);
        sprite.color = start;

        loaderMap.enabled = false;
    }
}
