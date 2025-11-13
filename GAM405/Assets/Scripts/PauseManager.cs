using UnityEngine;
using UnityEngine.InputSystem;

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
}
