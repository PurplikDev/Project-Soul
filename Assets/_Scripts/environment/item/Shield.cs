using roguelike.environment.entity;
using roguelike.environment.entity.player;
using roguelike.environment.entity.statsystem;

namespace roguelike.core.item {
    public class Shield : HandheldItem {

        StatModifier slowDown;

        public int MaxBlockAmount { get; private set; }
        public bool IsUp { get; private set; }
        public int WeaponTier { get; private set; }
        public int PerformedBlocksAmount { get; private set; }

        public Shield(string id, int value, float slowdownEffect, int weaponTier, int maxAmountOfBlocks, params StatModifier[] modifiers)
            : base(id, value, EquipmentType.OFF_HAND, false, modifiers) {
            IsUp = false;
            slowDown = new StatModifier(slowdownEffect, StatModifierType.ADDITIONAL, StatType.SPEED);
            WeaponTier = weaponTier;
            MaxBlockAmount = maxAmountOfBlocks;
        }

        public override void ItemAction(Entity user) {
            IsUp = !IsUp;

            if(IsUp) {
                user.Speed.AddModifier(slowDown);
                user.IsBlocking = true;
            } else {
                user.Speed.RemoveModifier(slowDown);
                user.IsBlocking = false;
            }

            if (user is Player player) {
                player.RotateEquipment();
            }
        }

        public void Blocked(Entity user) {
            PerformedBlocksAmount++;
            if(PerformedBlocksAmount >= MaxBlockAmount) {
                user.Speed.RemoveModifier(slowDown);
                user.IsBlocking = false;
                PerformedBlocksAmount = 0;
                if (user is Player player) {
                    player.RotateEquipment();
                }
            }
        }
    }
}