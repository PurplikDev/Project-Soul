using UnityEngine;

namespace roguelike.environment.entity {
    public class HostileEntity : Entity {
        public Transform EntityAim;

        protected void Update() {
            EntityAim.rotation = Rotation;
        }
    }
}