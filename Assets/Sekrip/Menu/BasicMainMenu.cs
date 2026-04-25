using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BasicMainMenu : MonoBehaviour
{
    [Header("Scene Name")]
    public string sceneName;
    public void LoadScene()
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Debug.Log("?????");
    }
}
