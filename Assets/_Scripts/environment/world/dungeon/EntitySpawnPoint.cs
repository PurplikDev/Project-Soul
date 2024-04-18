using roguelike.environment.entity;
using roguelike.system.manager;
using UnityEngine;

namespace roguelike.environment.world.dungeon {
    public class EntitySpawnPoint : MonoBehaviour {
        private void Start() {
            var enemies = Resources.LoadAll<GameObject>("prefabs/entities/hostile");
            var randomEnemy = enemies[Random.Range(0, enemies.Length)];

            var entity = randomEnemy.GetComponent<HostileEntity>();

            entity.DamageTier = (int)DungeonManager.Instance.Difficulty;
            entity.MaxHealth.AddModifier(new entity.statsystem.StatModifier(0.1f * (int)DungeonManager.Instance.Difficulty, environment.entity.statsystem.StatModifierType.ADDITIONAL, environment.entity.statsystem.StatType.HEALTH));
            entity.Speed.AddModifier(new entity.statsystem.StatModifier(0.1f * (int)DungeonManager.Instance.Difficulty, environment.entity.statsystem.StatModifierType.ADDITIONAL, environment.entity.statsystem.StatType.SPEED));

            Instantiate(randomEnemy, transform.position + new Vector3(0, 1, 0), new Quaternion(), transform.parent);

            Destroy(gameObject);
        }
    }
}