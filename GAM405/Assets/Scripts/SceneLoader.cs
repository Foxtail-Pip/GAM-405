using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void PressedStartButton(InputAction.CallbackContext inputData)
     {
        LoadMainScene();
        Debug.Log("Input Received");
     }

    public void LoadMainScene()
    {
        SceneManager.LoadScene("Greybox");
        Debug.Log("AttemptingLoad");
    }
}
