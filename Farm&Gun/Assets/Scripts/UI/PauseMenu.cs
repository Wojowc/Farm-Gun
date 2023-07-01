using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool IsGamePaused = false; // Static variable used for controlling whether game is paused or not
    private GameStateManager stateManager;
    public GameObject pauseMenuUI;
    private GameObject player;

    private void Awake()
    {
        stateManager = GameObject.Find("GameManager").GetComponent<GameStateManager>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused) Resume();
            else Pause();
        }
    }

    public void Resume() // Method used for resuming the game
    {
        player.GetComponent<PlayerMovement>().EnableMovement();
        if (stateManager.currentState.GetType() != typeof(GameStateMarket) && stateManager.currentState.GetType() != typeof(GameStateIntro))
        {
            Time.timeScale = 1f;
        }
        pauseMenuUI.SetActive(false);
        IsGamePaused = false;
        player.transform.Find("Attacks Point").GetComponent<PlayerAttack>().EnableAttack();
    }

    private void Pause() // Method used for pausing the game
    {
        player.GetComponent<PlayerMovement>().DisableMovement();
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        IsGamePaused = true;
        player.transform.Find("Attacks Point").GetComponent<PlayerAttack>().DisableAttack();
    }
}