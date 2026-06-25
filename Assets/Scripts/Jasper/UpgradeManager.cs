using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager upgradeManager;
    PointsManager pointsManager;

    [Header("Tower To Upgrade")]
    public Tower towerToUpgrade;
    [Header("UI Elements")]
    public GameObject towerPanel;
    [Header("Fire Rate Upgrade")]

    public Button fireRateUpgrade;
    public Text currentFireRate;
    public Text fireRateUpgradeDisplay;
    public Text fireRateUpgradeCost;
    [Header("Damage Upgrade")]

    public Button damageUpgrade;
    public Text currentDamage;
    public Text damageUpgradeDisplay;
    public Text damageUpgradeCost;


    void Awake()
    {
        if (upgradeManager == null)
        {
            upgradeManager = this;
        }
        else if (upgradeManager != this && upgradeManager != null)
        {
            Destroy(this);
        }
    }

    void Start()
    {
        pointsManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<PointsManager>();
        towerPanel.SetActive(false);

    }

    public void OpenUpgrades(Tower tower)
    {
        towerToUpgrade = tower;
        towerPanel.SetActive(true);
        UpdateDisplay();
    }
    void Update()
    {
        if (towerToUpgrade != null)
        {

            if (pointsManager.money >= towerToUpgrade.fireRateCost && towerToUpgrade.timesUpgradedFireRate < 3)
            {
                //turn on button
                fireRateUpgrade.interactable = true;
            }
            else
            {
                //turn off button
                fireRateUpgrade.interactable = false;
            }
            if (pointsManager.money >= towerToUpgrade.damageCost && towerToUpgrade.timesUpgradedDamage < 3)
            {
                //turn on button
                damageUpgrade.interactable = true;
            }
            else
            {
                //turn off button
                damageUpgrade.interactable = false;
            }
            if (towerPanel.activeSelf == true && (Input.GetKeyDown(KeyCode.T) || Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1)))
            {
                towerPanel.SetActive(false);
                towerToUpgrade = null;
            }
        }
        if (towerToUpgrade == null)
        {
            return;
        }

    }
    public void UpgradeFireRate()
    {
        if (pointsManager.money >= towerToUpgrade.fireRateCost)
        {
            towerToUpgrade.fireRate += towerToUpgrade.fireRateIncrease;
            pointsManager.money -= towerToUpgrade.fireRateCost;
            towerToUpgrade.fireRateCost += towerToUpgrade.fireRateCostIncrease;
            towerToUpgrade.timesUpgradedFireRate++;
            UpdateDisplay();
        }

    }
    public void UpgradeDamage()
    {
        if (pointsManager.money >= towerToUpgrade.damageCost)
        {
            towerToUpgrade.damage += towerToUpgrade.damageIncrease;
            pointsManager.money -= towerToUpgrade.damageCost;
            towerToUpgrade.damageCost += towerToUpgrade.damageCostIncrease;
            towerToUpgrade.timesUpgradedDamage++;
            UpdateDisplay();
        }

    }
    void UpdateDisplay()
    {
        currentDamage.text = $"Damage: {towerToUpgrade.damage}";
        if (towerToUpgrade.timesUpgradedDamage < 3)
        {
            damageUpgradeDisplay.text = $"New Damage: {towerToUpgrade.damage + towerToUpgrade.damageIncrease}";
            damageUpgradeCost.text = $"Cost: ${towerToUpgrade.damageCost}";
        }
        else
        {
            damageUpgradeDisplay.text = $"New Damage: MAXED";
            damageUpgradeCost.text = $"Cost: MAXED";
        }
            currentFireRate.text = $"Fire Rate: {towerToUpgrade.fireRate} per sec";
        if (towerToUpgrade.timesUpgradedFireRate < 3)
        {
            fireRateUpgradeDisplay.text = $"New Fire Rate: {towerToUpgrade.fireRate + 1} per sec";
            fireRateUpgradeCost.text = $"Cost : ${towerToUpgrade.fireRateCost}";
        }
        else
        {
            fireRateUpgradeDisplay.text = $"New Fire Rate: MAXED";
            fireRateUpgradeCost.text = $"Cost : MAXED";
        }
            
    }
}
