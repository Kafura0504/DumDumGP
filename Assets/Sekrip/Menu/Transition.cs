using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public enum Type
    {
        left, right, fade
    }


    [Header("Transition Type")]
    public Type typeOfTransition;
    public Canvas thisCanvas; //for setting sort order when loading child scene
    public GameObject MainMenu; //for hiding menu after childscene loaded
    public bool transFinishedNLoaded = false;
    private float duration = 0.7f;
    private Vector2 screenSize = new(Screen.width, Screen.height);
    private Image opacity;

    public void FadeNext(string sceneName)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Transitioning(Type.fade, sceneName));
    }   
    public void SlideLeftNext(string sceneName)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Transitioning(Type.left, sceneName));
    }   
    public void SlideRightNext(string sceneName)
    {
        this.gameObject.SetActive(true);
        StartCoroutine(Transitioning(Type.right, sceneName));
    }

    public IEnumerator Transitioning(Type transtype, string nextScene)
    {
        if (transtype == Type.left || transtype == Type.right)
        {
            Vector2 thisPos = this.gameObject.transform.position;
            Vector2 finalPos = thisPos;
            Vector2 startPos;
            Vector2 exitPos;
            switch (transtype)
            {
                case Type.left:

                    //enter
                    startPos = new(thisPos.x + screenSize.x, thisPos.y);
                    yield return StartCoroutine(NextSlide(startPos, finalPos));
                    thisCanvas.sortingOrder = 100; //hide childscene while being loaded

                    //do something

                    //exit
                    yield return new WaitForSeconds(0.5f);
                    exitPos = new(thisPos.x - screenSize.x, thisPos.y);
                    yield return StartCoroutine(NextSlide(finalPos, exitPos));

                    //reset
                    thisCanvas.sortingOrder = 0;
                    this.gameObject.SetActive(false);
                    this.gameObject.transform.position = screenSize/2;
                    break;

                case Type.right:

                    //enter
                    startPos = new(thisPos.x - screenSize.x, thisPos.y);
                    yield return StartCoroutine(NextSlide(startPos, finalPos));
                    thisCanvas.sortingOrder = 100; //hide childscene while being loaded

                    //do something

                    //exit
                    yield return new WaitForSeconds(0.5f);
                    exitPos = new(thisPos.x + screenSize.x, thisPos.y);
                    yield return StartCoroutine(NextSlide(finalPos, exitPos));

                    //reset
                    thisCanvas.sortingOrder = 0;
                    this.gameObject.SetActive(false);
                    this.gameObject.transform.position = screenSize/2;
                    break;
            }
        }
        else
        {   
            //enter
            yield return ImFaded(0f, 1f);
            thisCanvas.sortingOrder = 100; //hide childscene while being loaded

            //do somthing

            //exit
            yield return new WaitForSeconds(0.5f);
            yield return ImFaded(1f, 0f);

            //reset
            thisCanvas.sortingOrder = 0;
            this.gameObject.SetActive(false);
            opacity = this.gameObject.GetComponent<Image>();
            Color transparent = opacity.color;
            transparent.a = 1f;
            opacity.color = transparent;
        }
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator NextSlide(Vector2 start, Vector2 end)
    {
        this.gameObject.transform.position = start;
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float tick = time/duration;
            this.gameObject.transform.position = Vector2.Lerp(start, end, tick);
            yield return null;
        }
        
        this.gameObject.transform.position = end;
    }
    IEnumerator ImFaded(float start, float end)
    {
        
            opacity = this.gameObject.GetComponent<Image>();
            Color transparent = opacity.color;
            transparent.a = start;
            float setAlpha = transparent.a;
            float time = 0f;
            while (time < duration)
            {
                time += Time.deltaTime;
                float tick = time/duration;
                transparent.a = Mathf.Lerp(setAlpha, end, tick);
                opacity.color = transparent;
                yield return null;
            }
    }

}
