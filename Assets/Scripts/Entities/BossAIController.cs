using io.purplik.ProjectSoul.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace io.purplik.ProjectSoul.Entity
{
    public class BossAIController : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            var player = other.GetComponent<PlayerEntity>();
            if (player != null)
            {
                GetComponentInParent<NavMeshAgent>().destination = player.transform.position;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var player = other.GetComponent<PlayerEntity>();
            if (player != null)
            {
                Debug.Log("BossThrow");
            }
        }
    }
}