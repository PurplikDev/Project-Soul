using roguelike.environment.entity;
using roguelike.environment.entity.special;
using roguelike.system.manager;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    public GameObject Projectile;

    private void Start() {
        InvokeRepeating(nameof(Attack), 2.5f, 2.5f);
        GetComponent<HostileEntity>().LookDirection = new Vector3(-1, 0, -1);
    }

    internal void Attack() {
        var entity = GetComponent<HostileEntity>();
        var rotation = entity.Rotation;

        var projectile = Instantiate(Projectile, new Vector3(transform.position.x, GameManager.Player.transform.position.y, transform.position.z), rotation);
        var proj_ent = projectile.GetComponent<EntityProjectile>();
        proj_ent.owner = entity;
    }
}
