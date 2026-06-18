using UnityEngine;

public class Pause : MonoBehaviour
{
    /// <summary>
    /// Pauses the time using UnityEngine.time
    /// </summary>
    public void PauseTime()
    {
        //sets the timescale in the time library to 0
        Time.timeScale = 0;
    }
    /// <summary>
    /// Unpauses the time using UnityEngine.time
    /// </summary>
    public void UnpauseTime()
    {
        //sets the timescale in the time library to 1
        Time.timeScale = 1f;
    }
}
