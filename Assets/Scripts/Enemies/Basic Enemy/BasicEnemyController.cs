using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyController : MonoBehaviour
{
    public float lookRadius = 10f;

    Transform playerTarget;
    NavMeshAgent navMeshAgent;

    void Start() {
        playerTarget = PlayerManager.playerManager.playerObject.transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        float distance = Vector3.Distance(playerTarget.position, transform.position);
        if(distance <= lookRadius)  {
            navMeshAgent.SetDestination(playerTarget.position);
        }
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
