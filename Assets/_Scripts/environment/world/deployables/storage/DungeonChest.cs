using roguelike.core.item;
using roguelike.core.item.loottable;
using roguelike.core.utils.mathematicus;
using System.Collections.Generic;

namespace roguelike.environment.world.deployable.workstation {
    public class DungeonChest : StorageCrate {

        public LootTable Loot;

        protected override void Awake() {
            StationInventory = new List<ItemStack>();
            for (int i = 0; i < 12; i++) {
                if (Mathematicus.ChanceIn(75f)) {
                    StationInventory.Add(ItemStack.EMPTY);
                } else {
                    StationInventory.Add(Loot.GetRandomLoot());
                }
            }
        }
    }
}
