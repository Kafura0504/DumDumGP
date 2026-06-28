using UnityEngine;
using UnityEngine.UI;

public class LoadMapData : MonoBehaviour
{
    [Header("MapData")]
    public MapData data;
    [Header("Scene data")]
    public ScriptableScene scene;
    [Header("Place Holder NPC Location")]
    public RectTransform Left;
    public RectTransform Mid;
    public RectTransform Right;
    [Header("NPC Game Object")]
    public Image NpcOne;
    public Image NpcTwo;
    public GameObject bottombar;
    [Header("Baground")]
    public Image Background;

    void OnEnable()
    {
        if (scene != null && data != null)
        {
            Debug.LogError("Just put one Scriptable data to load the scene");
            return;
        }

        if (data == null)
        {
            NpcOne.gameObject.SetActive(false);
            NpcTwo.gameObject.SetActive(false);
            BottomController controller = bottombar.GetComponent<BottomController>();
            controller.scene = scene;
            bottombar.SetActive(true);
            
            if (scene.audio != null)
            {    
            GameManager.Instance.Aud.clip = scene.audio;
            GameManager.Instance.Aud.Play();
            }

            this.enabled = false;
            return;
        }

        bottombar.SetActive(false);
        Background.sprite = data.Background;
        if (data.NpcOne)
        {
            NpcOne.gameObject.SetActive(true);
            //Changing position
            switch (data.speakerOnePosition)
            {
                case MapData.position.left:
                NpcOne.rectTransform.position = Left.position;
                break;

                case MapData.position.mid:
                NpcOne.rectTransform.position = Mid.position;
                break;

                case MapData.position.right:
                NpcOne.rectTransform.position = Right.position;
                break;
            }

            //Changing Expression
            switch (data.speakerOneExpression)
            {
                case MapData.ekspresi.normal :
                NpcOne.sprite = data.speakerOne.ekspresi.Normal;
                break;

                case MapData.ekspresi.cautious :
                NpcOne.sprite = data.speakerOne.ekspresi.Cautious;
                break;

                case MapData.ekspresi.relieved :
                NpcOne.sprite = data.speakerOne.ekspresi.relieved;
                break;

                case MapData.ekspresi.terrified :
                NpcOne.sprite = data.speakerOne.ekspresi.terrified;
                break;
            }
        }
        else
        {
            NpcOne.gameObject.SetActive(false);
        }

        //Npc kedua
        if (data.NpcTwo)
        {
            NpcTwo.gameObject.SetActive(true);
            //Changing position
            switch (data.speakerTwoPosition)
            {
                case MapData.position.left:
                NpcTwo.rectTransform.position = Left.position;
                break;

                case MapData.position.mid:
                NpcTwo.rectTransform.position = Mid.position;
                break;

                case MapData.position.right:
                NpcTwo.rectTransform.position = Right.position;
                break;
            }

            //Changinng Expression
            switch (data.speakerTwoExpression)
            {
                case MapData.ekspresi.normal :
                NpcTwo.sprite = data.speakerTwo.ekspresi.Normal;
                break;

                case MapData.ekspresi.cautious :
                NpcTwo.sprite = data.speakerTwo.ekspresi.Cautious;
                break;

                case MapData.ekspresi.relieved :
                NpcTwo.sprite = data.speakerTwo.ekspresi.relieved;
                break;

                case MapData.ekspresi.terrified :
                NpcTwo.sprite = data.speakerTwo.ekspresi.terrified;
                break;
            }
        }
        else
        {
            NpcTwo.gameObject.SetActive(false);
        }

        GameManager.Instance.Aud.clip = data.BGM;
        GameManager.Instance.Aud.Play();
    }
}
