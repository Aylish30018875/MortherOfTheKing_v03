using UnityEngine;
using System.Collections.Generic;

public class Tower : MonoBehaviour
{
    public float range = 3;
    public float fireRate = 1;
    public GameObject projectilePrefab;
    public Transform firePoint;
    public int damage;
    public float speed;

    //Cost of the tower, used for purchasing and upgrading
    public int cost = 10;

    private float fireCooldown;

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
