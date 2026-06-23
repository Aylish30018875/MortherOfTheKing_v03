using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
    public int lives = 5;
    public Text livesDisplay;
    public string livesDisplayString;
    public Image[] hearts;
    public Sprite[] display;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null && instance != this)
        {
            Destroy(this);
        }
        UpdateUI();
    }

    public void TakeDamage(int damageAmount)
    {
        lives -= damageAmount;
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i >= lives)
            {
                hearts[i].sprite = display[0];
            }
            else
            {
                hearts[i].sprite = display[1];
            }
        }
        livesDisplayString = $"Lives: {lives}";
        livesDisplay.text = livesDisplayString;
    }
    void Update()
    {
          

    }
}
