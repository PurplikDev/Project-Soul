using UnityEngine;

public class entityAttack : MonoBehaviour
{

    public Transform aimPoint;
    private EntityStats targetStats;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(aimPoint.position, aimPoint.forward);
    }

    void Attack()
    {
        RaycastHit hit;
        Physics.Raycast(aimPoint.position, aimPoint.forward, out hit, 10f);
        
        if (hit.transform != null) {
            targetStats = hit.transform.GetComponent<EntityStats>();
            targetStats.Damage(10, "true");
            Debug.Log(hit.transform.name + " " + targetStats.health.ToString());
        }
    }

    void Update() {
        RaycastHit hit;
        Physics.Raycast(aimPoint.position, aimPoint.forward, out hit, 5f);

        if(hit.transform != null && hit.transform.CompareTag("Player")) {
            transform.GetComponent<Animator>().SetTrigger("attacked");
        }
    }
}
