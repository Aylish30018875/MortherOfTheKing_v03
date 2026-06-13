using UnityEngine;
using UnityEngine.UI;

public class PointsManager : MonoBehaviour
{
    public int money = 10;
    public Text moneyDisplay;
    public string moneyDisplayString;

    void Update()
    {
        moneyDisplayString = $"{money} coins";
        moneyDisplay.text = moneyDisplayString;
    }
}
