using UnityEngine;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    public float speed = 2;
    public int reward = 1;
    public int health = 1;
    public Transform[] wayPoints;
    public List<EnemyAnimation> enemyAnimation = new List<EnemyAnimation>();
    public int currentWayPoint = 0;
    public DirectionWaypoint directionWaypoint;
    public HealthManager healthManager;
    public Color enemyColor = Color.white;

    void Awake()
    {
        healthManager = GameObject.FindGameObjectWithTag("HealthManager").GetComponent<HealthManager>();
      //  enemyAnimation = transform.GetChild(0).GetComponent<EnemyAnimation>();
        foreach (Transform childObject in GetComponentsInChildren<Transform>())
        {
            EnemyAnimation temp = childObject.GetComponent<EnemyAnimation>();
            enemyAnimation.Add(temp);
        }

    }
    void Start()
    {
        
    }

    private void Update()
    {
        if (wayPoints == null || wayPoints.Length == 0)
        {
            return;
        }
        Transform target = wayPoints[currentWayPoint];
        Vector3 dir = (target.position - transform.position).normalized;

        transform.position += dir * speed * Time.deltaTime;

        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            currentWayPoint++;
            directionWaypoint = wayPoints[currentWayPoint - 1].GetComponent<DirectionWaypoint>();
            foreach (EnemyAnimation anim in enemyAnimation)
            {
                if (anim != null)
                {
                    anim.Walk(directionWaypoint.direction, directionWaypoint.value);
                }
            }
            if (currentWayPoint >= wayPoints.Length)
            {
                healthManager.lives -= 1;
                Destroy(gameObject);
            }
        }
    }
}
