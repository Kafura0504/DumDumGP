using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class OpenInventory : MonoBehaviour
{
    [Header("Control input")]
    public InputActionReference input;
    private bool isOpen;

    void clicked(InputAction.CallbackContext ctx)
    {
        if (!isOpen)
        {
            SceneManager.LoadSceneAsync("inventory", LoadSceneMode.Additive);
            isOpen = true;
            GameManager.Instance.state = GameState.Paused;
        }
        else
        {
            SceneManager.UnloadSceneAsync("inventory");
            isOpen = false;
            GameManager.Instance.state = GameState.Standby;
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
}
