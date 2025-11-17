using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;

    private void OnEnable()
    {
        EventManager.TogglePause += TogglePauseMenu;
    }

    private void OnDisable()
    {
        EventManager.TogglePause -= TogglePauseMenu;
    }

    private void TogglePauseMenu(bool paused)
    {
        Time.timeScale = paused ? 0f : 1f; //seems to store inputs and change states even when time stopped?
        pauseMenu.SetActive(paused);
    }
}
