using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int lives = 5;
    public Text livesDisplay;
    public string livesDisplayString;

    void Update()
    {
        livesDisplayString = $"Lives: {lives}";
        livesDisplay.text = livesDisplayString;


    }
}
