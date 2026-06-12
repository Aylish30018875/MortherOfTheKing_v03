using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public int projectileSpeed;
    public int projectileDamage;

    private Tower towerScript;

    public GameObject towerPanel;


    void Start()
    {
        towerPanel.SetActive(false);
    }
    void Update()
    {
        if (towerPanel.activeSelf == false && Input.GetKeyDown(KeyCode.T))
        {
            towerPanel.SetActive(true);
        }
        else if (towerPanel.activeSelf == true && Input.GetKeyDown(KeyCode.T))
        {
            towerPanel.SetActive(false);
        }
    }
}
