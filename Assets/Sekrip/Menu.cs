using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Menu : MonoBehaviour
{
    [Header("Control input")]
    public InputActionReference input;
    private bool isOpen;

    private GameState state;

    void clicked(InputAction.CallbackContext ctx)
    {
        if (!isOpen)
        {
            SceneManager.LoadSceneAsync("inventory", LoadSceneMode.Additive);
            isOpen = true;
            state = GameState.Paused;
        }
        else
        {
            SceneManager.UnloadSceneAsync("inventory");
            isOpen = false;
            state = GameState.Standby;
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
