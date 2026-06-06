using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class OpenInventory : MonoBehaviour
{
    [Header("Control input")]
    public InputActionReference input;
    public GameObject mainmenu;
    private bool isOpen = false;

    void Clicked(InputAction.CallbackContext ctx)
    {
        isOpen = !isOpen;
        // if (!mainmenu.activeSelf)
        switch (isOpen)
        {

            case true:
                SceneManager.LoadSceneAsync("inventory", LoadSceneMode.Additive);
                Debug.Log("why");
                GameManager.Instance.state = GameState.Paused;
                break;
            case false:
                SceneManager.UnloadSceneAsync("inventory");
                Debug.Log("false");
                GameManager.Instance.state = GameState.Standby;
                break;
        }
    }

    public void OnEnable()
    {
        input.action.started += Clicked;
        input.action.Enable();
    }
    public void OnDisable()
    {
        input.action.started -= Clicked;
        input.action.Disable();
    }
}
