using UnityEngine;

public class GameEndManager : MonoBehaviour
{
    //References the health manager script so it can check the lives variable
    public HealthManager healthManager;

    //References the wave manager script so it can check what wave the player is on
    public WaveManager waveManager;

    //Panels that will show when the player wins or loses
    public GameObject youLosePanel;
    public GameObject youWinPanel;

    //Amount of waves needed before the player wins
    public int wavesToWin = 2;
    //Prevents the win or loss code from running more than once
    private bool gameEnded = false;

    void Start()
    {
        //Hides both panels when the game starts
        youLosePanel.SetActive(false);
        youWinPanel.SetActive(false);
        //Makes sure the game is running at normal speed
        Time.timeScale = 1f;
    }

    void Update()
    {
        //Stops checking if the game has already ended
        if (gameEnded == true)
        {
            return;
        }

        //If the player has no lives left, run the lose game function
        if (healthManager.lives <= 0)
        {
            LoseGame();
        }

        //If the player has completed enough waves and a wave is not currently running, run the win game function
        if (waveManager.CurrentWaveIndex >= wavesToWin && waveManager.IsRunning == false)
        {
            WinGame();
        }
    }

    void LoseGame()
    {
        //Marks the game as ended
        gameEnded = true;
        //Shows the lose panel
        youLosePanel.SetActive(true);
        //Pauses the game
        Time.timeScale = 0f;
    }

    void WinGame()
    {
        //Marks the game as ended
        gameEnded = true;
        //Shows the win panel
        youWinPanel.SetActive(true);
        //Pauses the game
        Time.timeScale = 0f;
    }
}