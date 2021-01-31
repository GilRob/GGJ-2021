using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public List<Transform> potrolPoints;
    public float playerInSightDistance = 5f;
    public float hearWalkingDistance;
    public float hearCrouchWalkDistance;
    public float potrollingSpeed = 5f;
    public float chasingSpeed = 10f;
    public float idelTime = 2f;
    public float viewAngle;
    
    NavMeshAgent navMeshAgent;
    Vector3 patrolPoint;
    bool patrolPointSet;
    float idelTimer;
    bool playerInSight;
    Animator animator;

    //Gil Stuff for Sounds
    public AudioSource chaseAudioSource;
    public AudioSource stealthAudioSource;

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
        this.gameObject.GetComponent<SphereCollider>().radius = playerInSightDistance;
        animator = this.gameObject.GetComponent<Animator>();
        animator.SetBool("animWalk", true);
        animator.SetBool("animRun", false);
    }

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
        stealthAudioSource.mute = false;
        chaseAudioSource.mute = true;

        idelTimer -= Time.deltaTime;

        if (Vector3.Distance(player.transform.position, this.transform.position) < playerInSightDistance)
        {
            if (playerInSight)
            {
                //this.gameObject.GetComponent
                idelTimer = idelTime;
                enemyState = EnemyState.Chase;
                navMeshAgent.speed = chasingSpeed;
                playerInSight = false;//??????????
            }
            if (player.GetComponent<PlayerController>().isRunning)
            {
                idelTimer = idelTime;
                enemyState = EnemyState.Chase;
                navMeshAgent.speed = chasingSpeed;
            }
            else if (player.GetComponent<PlayerController>().isCrouching && !player.GetComponent<PlayerController>().isStop)
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < hearCrouchWalkDistance)
                {
                    idelTimer = idelTime;
                    enemyState = EnemyState.Chase;
                    navMeshAgent.speed = chasingSpeed;
                }
            }
            else if (player.GetComponent<PlayerController>().isStop)
            {
                // when enemy is not look at player and player is not moving, player won't be detect
            }
            else
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < hearWalkingDistance)
                {
                    idelTimer = idelTime;
                    enemyState = EnemyState.Chase;
                    navMeshAgent.speed = chasingSpeed;
                }
            }
        }

        if (idelTimer <= 0)
        {
            idelTimer = idelTime;
            enemyState = EnemyState.Patrol;
            navMeshAgent.speed = potrollingSpeed;
            animator.SetBool("animWalk", true);
            animator.SetBool("animRun", false);
        }
    }

    void EnemyPatrolling()
    {
        stealthAudioSource.mute = false;
        chaseAudioSource.mute = true;

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

        if(Vector3.Distance(player.transform.position, this.transform.position) < playerInSightDistance)
        {
            if (playerInSight)
            {
                patrolPointSet = false;
                enemyState = EnemyState.Chase;
                navMeshAgent.speed = chasingSpeed;
                animator.SetBool("animWalk", false);
                animator.SetBool("animRun", true);
            }
            if (player.GetComponent<PlayerController>().isRunning)
            {
                patrolPointSet = false;
                enemyState = EnemyState.Chase;
                navMeshAgent.speed = chasingSpeed;
                animator.SetBool("animWalk", false);
                animator.SetBool("animRun", true);
            }
            else if (player.GetComponent<PlayerController>().isCrouching && !player.GetComponent<PlayerController>().isStop)
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < hearCrouchWalkDistance)
                {
                    patrolPointSet = false;
                    enemyState = EnemyState.Chase;
                    navMeshAgent.speed = chasingSpeed;
                    animator.SetBool("animWalk", false);
                    animator.SetBool("animRun", true);
                }
            }
            else if (player.GetComponent<PlayerController>().isStop)
            {
                // when enemy is not look at player and player is not moving, player won't be detect
            }
            else
            {
                if (Vector3.Distance(player.transform.position, this.transform.position) < hearWalkingDistance)
                {
                    patrolPointSet = false;
                    enemyState = EnemyState.Chase;
                    navMeshAgent.speed = chasingSpeed;
                    animator.SetBool("animWalk", false);
                    animator.SetBool("animRun", true);
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direct = other.transform.position - transform.position;
            float angle = Vector3.Angle(direct, transform.forward);
            if(angle < viewAngle * 0.5f)
            {
                RaycastHit hit;

                if(Physics.Raycast(transform.position + transform.up, direct.normalized, out hit, playerInSightDistance))
                {
                    if(hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                    }
                }
            }
        }
    }

    void EnemyChasing()
    {
        chaseAudioSource.mute = false;
        stealthAudioSource.mute = true;

        if (playerInSight && Vector3.Distance(player.transform.position, this.transform.position) < playerInSightDistance)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else if(player.GetComponent<PlayerController>().isRunning && Vector3.Distance(player.transform.position, this.transform.position) < playerInSightDistance)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else if (player.GetComponent<PlayerController>().isJumping && Vector3.Distance(player.transform.position, this.transform.position) < playerInSightDistance)
        {
            player.GetComponent<PlayerController>().isJumping = false;
            navMeshAgent.SetDestination(player.transform.position);
        }
        else if(!player.GetComponent<PlayerController>().isStop && !player.GetComponent<PlayerController>().isCrouching && Vector3.Distance(player.transform.position, this.transform.position) < hearWalkingDistance)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else if (!player.GetComponent<PlayerController>().isStop && player.GetComponent<PlayerController>().isCrouching && Vector3.Distance(player.transform.position, this.transform.position) < hearCrouchWalkDistance)
        {
            navMeshAgent.SetDestination(player.transform.position);
        }
        else
        {
            if (Vector3.Distance(this.transform.position, navMeshAgent.destination) < 3f)
            {
                enemyState = EnemyState.Idle;
            }
        }
    }
}
