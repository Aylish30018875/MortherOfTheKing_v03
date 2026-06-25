using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameEndManager : MonoBehaviour
{
    float timer;
    public static int score = 0;
    //References the health manager script so it can check the lives variable
    public HealthManager healthManager;

    //References the wave manager script so it can check what wave the player is on
    public WaveManager waveManager;

    //Panels that will show when the player wins or loses
    public GameObject gamOverPanel;
    public Text panelTitle;
    public Text gameTimer;
    public Text gameScore;
    public Text gameLives;
   // public GameObject youWinPanel;

    //Amount of waves needed before the player wins
    public int wavesToWin = 2;
    //Prevents the win or loss code from running more than once
    private bool gameEnded = false;

    void Start()
    {
        //Hides both panels when the game starts
        gamOverPanel.SetActive(false);
        //Makes sure the game is running at normal speed
        Time.timeScale = 1f;
        score = 0; 
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
        if (waveManager.CurrentWaveIndex >= wavesToWin && waveManager.IsRunning == false && FindObjectsByType<Enemy>(FindObjectsInactive.Include,FindObjectsSortMode.None).Length == 0)
        {
            WinGame();
        }
    }
    void EndGame()
    {
        timer = Time.timeSinceLevelLoad;
        int minutes = (int)(Time.timeSinceLevelLoad / 60);
        int seconds = (int)(Time.timeSinceLevelLoad % 60);
        string display = $"{minutes:00}:{seconds:00}";
        gameTimer.text = $"Time: {display}";
        gameScore.text = $"Score: {score}";
        gameLives.text = $"Lives: {healthManager.lives}";
        //Marks the game as ended
        gameEnded = true;
        //Shows the lose panel
        gamOverPanel.SetActive(true);
        //Pauses the game
        Time.timeScale = 0f;
    }
    void LoseGame()
    {
        panelTitle.text = $"You Lost";
        EndGame();
    }

    void WinGame()
    {
        panelTitle.text = $"You Won";
        EndGame();
    }
}