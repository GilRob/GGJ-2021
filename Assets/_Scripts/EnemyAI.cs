using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public List<Transform> potrolPoints;
    public float playerInSightDistance = 5f;
    public float potrollingSpeed = 5f;
    public float chasingSpeed = 10f;
    public float idelTime = 2f;
    
    NavMeshAgent navMeshAgent;
    Vector3 patrolPoint;
    bool patrolPointSet;
    float idelTimer;

    enum EnemyState
    {
        Idle,
        Patrol,
        Chase
    }
    
    EnemyState enemyState;

    void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
        enemyState = EnemyState.Patrol;
        patrolPointSet = false;
        idelTimer = idelTime;
        navMeshAgent.speed = potrollingSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        switch (enemyState)
        {
            case EnemyState.Idle:
                EnemyIdle();
                break;
            case EnemyState.Patrol:
                EnemyPatrolling();
                break;
            case EnemyState.Chase:
                EnemyChasing();
                break;
        }
    }

    void EnemyIdle()
    {
        idelTimer -= Time.deltaTime;

        if (Vector3.Distance(player.position, this.transform.position) < playerInSightDistance)
        {
            idelTimer = idelTime;
            enemyState = EnemyState.Chase;
            navMeshAgent.speed = chasingSpeed;
        }

        if (idelTimer <= 0)
        {
            idelTimer = idelTime;
            enemyState = EnemyState.Patrol;
            navMeshAgent.speed = potrollingSpeed;
        }
    }

    void EnemyPatrolling()
    {
        if (!patrolPointSet)
        {
            int random = Random.Range(0, 4);
            patrolPoint = potrolPoints[random].position;
            patrolPointSet = true;
        }
        else
        {
            navMeshAgent.SetDestination(patrolPoint);
        }

        Vector3 distance = this.transform.position - patrolPoint;

        if (distance.magnitude < 1f)
            patrolPointSet = false;

        if(Vector3.Distance(player.position, this.transform.position) < playerInSightDistance)
        {
            patrolPointSet = false;
            enemyState = EnemyState.Chase;
            navMeshAgent.speed = chasingSpeed;
            if (player.GetComponent<PlayerController>().isRunning)
            {

            }
            else if (player.GetComponent<PlayerController>().isCrouching)
            {

            }
            else if (player.GetComponent<PlayerController>().isStop)
            {

            }
            else
            {

            }
        }
    }

    void EnemyChasing()
    {
        if (Vector3.Distance(player.position, this.transform.position) > playerInSightDistance)
        {
            if(Vector3.Distance(this.transform.position, navMeshAgent.destination) < 3f)
            {
                enemyState = EnemyState.Idle;
            }
        }
        else
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
    }
}
