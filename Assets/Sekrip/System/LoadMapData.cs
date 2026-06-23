using UnityEngine;
using UnityEngine.UI;

public class LoadMapData : MonoBehaviour
{
    [Header("MapData")]
    public MapData data;
    [Header("Place Holder NPC Location")]
    public RectTransform Left;
    public RectTransform Mid;
    public RectTransform Right;
    [Header("NPC Game Object")]
    public Image NpcOne;
    public Image NpcTwo;
    [Header("Baground")]
    public Image Background;

    void OnEnable()
    {
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
    }
    void Awake() //ini ditaro di OnEnable. kucontohin di Awake soalnya OnEnable udah dipake yang lain
    {
        BottomController.done += awikwok;
    }   

    void awikwok()
    {
        //ini code yang dieksekusi kalau sudah done
        Debug.Log("Sudah Done Mas");
    }
}
