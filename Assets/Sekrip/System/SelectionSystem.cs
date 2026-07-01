using TMPro;
using UnityEngine;

public class SelectionSystem : MonoBehaviour
{
    [Header("argument auto fill")]
    public ScriptableScene scene;
    public string dialogue;
    [Header("Game Object")]
    public TextMeshProUGUI dialogueTMP;
    public GameObject SelectionBox;
    public GameObject BottomBar;

    [Header("don't Fill this")]
    public string unlockFlag;
    public void Select()
    {
        BottomController controller = BottomBar.GetComponent<BottomController>();
        if (!string.IsNullOrEmpty(unlockFlag))
        {
            GameManager.Instance.FlagsChoice[unlockFlag] = true;
        }
        controller.scene = scene;
        BottomBar.SetActive(true);
        SelectionBox.SetActive(false);
    }

    void OnEnable()
    {
        dialogueTMP.SetText(dialogue);
    }
    void OnDisable()
    {
        unlockFlag = "";
        gameObject.SetActive(false);
    }
}
