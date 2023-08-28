using System.Collections.Generic;

namespace roguelike.enviroment.item.Container {
    public class Container {
        
        public List<ItemStack> Items;
        
        public Container(int slotCount) { 
            Items = new List<ItemStack>(24);
        }
    }
}