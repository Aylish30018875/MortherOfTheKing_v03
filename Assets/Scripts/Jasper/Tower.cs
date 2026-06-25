using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Tower : MonoBehaviour, IPointerClickHandler
{
    [Header("Tower")]
    //Cost of the tower, used for purchasing and upgrading
    [Tooltip("Cost of the Tower")] public int cost = 10;
    [Tooltip("Tower Attack Range")] public float range = 3;

    [Header("Projectile")]
    [Tooltip("Bullet")] public GameObject projectilePrefab;
    [Tooltip("Spawn Point of Bullet")] public Transform firePoint;
    [Tooltip("Damage that Bullet Does")] public int damage;
    [Tooltip("Projectile Speed")] public float speed;


    [Header("Tower Fire Rate Upgrade")]
    [Tooltip("Tower Fire Rate Per Second")] public float fireRate = 1;
    [Tooltip("Price to Upgrade the Fire Rate")] public int fireRateCost = 10;
    [Tooltip("After Purchace how much does the price increase by")] public int fireRateCostIncrease = 10;
    [Tooltip("The amount that the damage increases by")] public int fireRateIncrease = 1;

    private float fireCooldown;
    public int timesUpgradedFireRate = 0;

    [Header("Tower Damage Upgrade")]
    [Tooltip("Price to Upgrade the damage")] public int damageCost = 10;
    [Tooltip("After Purchace how much does the price increase by")] public int damageCostIncrease = 10;
    [Tooltip("The amount that the damage increases by")] public int damageIncrease = 10;
    public int timesUpgradedDamage = 0;




    private void Update()
    {
        fireCooldown -= Time.deltaTime;
        Enemy target = FindBestTarget();
        if (target != null && fireCooldown <= 0)
        {
            Shoot(target);
            fireCooldown = 1 / fireRate;
        }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == 0)
        {
            OnInteracted();
        }
       
    }
    void OnInteracted()
    {
        Debug.Log("Test Interaction");
        UpgradeManager.upgradeManager.OpenUpgrades(this);
    }
    Enemy FindBestTarget()
    {
        Enemy[] enemies = GameObject.FindObjectsByType<Enemy>(FindObjectsSortMode.None);
        Enemy best = null;
        float bestProgress = -1;

        foreach (Enemy enemy in enemies)
        {
            float dist = Vector2.Distance(transform.position, enemy.transform.position);
            if (dist < range)
            {
                if (enemy.currentWayPoint > bestProgress)
                {
                    bestProgress = enemy.currentWayPoint;
                    best = enemy;
                }
            }
        }
        return best;
    }

    void Shoot(Enemy enemy)
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Projectile pr = projectile.GetComponent<Projectile>();
        pr.damageNumber = damage;
        pr.speed = speed;
        pr.target = enemy.transform;
    }
}
