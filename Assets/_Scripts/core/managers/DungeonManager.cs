using roguelike.system.singleton;
using roguelike.enviroment.world.dungeon;

namespace roguelike.system.manager {
    public class DungeonManager : Singleton<DungeonManager> {
        private DungeonGenerator generator;
        private DungeonSpawner spawner;

        protected override void Awake() {
            generator = new DungeonGenerator();
            generator.GenerateDungeon(11);
            base.Awake();
        }
    } 
}

