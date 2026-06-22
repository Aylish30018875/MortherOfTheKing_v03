using UnityEngine;

public class LevelSpeed : MonoBehaviour
{
    /// <summary>
    /// Allows the player to double the time scale of the game, making everything move faster, and reset it back to normal speed.
    /// <summary>
    public void DoubleTime()
    {
        Debug.Log("Double Time");
        //Doubles time scale
        Time.timeScale = 2f;
    }

    public void ResetTime()
    {
        Debug.Log("Reset Time");
        //Resets time scale back to normal
        Time.timeScale = 1f;
    }
}
