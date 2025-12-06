using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public bool paused;
    public InputActionReference pausePressed;

    public void TogglePause(InputAction.CallbackContext callbackContext)
    {
        paused = !paused;
        EventManager.InvokeTogglePause(paused);
    }

    public void OnEnable()
    {
        if (pausePressed != null) 
        {
            pausePressed.action.performed += TogglePause;
            pausePressed.action.Enable();
        }
    }

    public void OnDisable()
    {
        if (pausePressed != null)
        {
            pausePressed.action.performed += TogglePause;
            pausePressed.action.Disable();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quitted...");
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene("Greybox");
        Debug.Log("AttemptingLoad");
        Time.timeScale = 1.0f;
    }
}

