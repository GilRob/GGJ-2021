using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    NavMeshAgent navMeshAgent;

    void Start()
    {
        navMeshAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(player.transform.position);
    }
}
