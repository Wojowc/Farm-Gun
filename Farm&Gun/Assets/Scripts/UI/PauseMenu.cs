using UnityEngine;
using UnityEngine.Serialization;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false; // Static variable used for controlling whether game is paused or not
    public GameObject pauseMenuUI;

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume() // Method used for resuming the game
    {
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        IsGamePaused = false;
    }

    void Pause() // Method used for pausing the game
    {
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        IsGamePaused = true;
    }
}