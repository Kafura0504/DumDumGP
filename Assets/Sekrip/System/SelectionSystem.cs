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
    public void Select()
    {
        BottomController controller = BottomBar.GetComponent<BottomController>();
        controller.scene = scene;
        BottomBar.SetActive(true);
        SelectionBox.SetActive(false);
    }

    void OnEnable()
    {
        dialogueTMP.SetText(dialogue);
    }
}
