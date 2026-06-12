using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public int money;
    public Text moneyDisplay;
    public string moneyDisplayString;

    void Update()
    {
        moneyDisplayString = $"{money}";
        moneyDisplay.text = moneyDisplayString;
    }
}
