using roguelike.enviroment.entity.player.inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace roguelike.enviroment.entity.player {
    public class Player : Entity {
        public Inventory inventory = new Inventory();
    }
}
