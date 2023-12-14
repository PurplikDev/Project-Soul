using roguelike.enviroment.entity.statsystem;
using roguelike.system.singleton;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace roguelike.core.item {
    public class ItemManager : PersistentSingleton<ItemManager> {

        private static Dictionary<string, Item> _itemDatabase = new Dictionary<string, Item>();

        public static List<Sprite> ItemIcons;
        public static Sprite MissingTexture;

        protected override void Awake() {
            base.Awake();
            MissingTexture = Resources.Load<Sprite>("sprites/missing_texture");
            ItemIcons = Resources.LoadAll<Sprite>("sprites/items").ToList();
            RegisterItems();
        }
      
        public static Sprite GetSpriteByID(string id) {
            var sprite =  ItemIcons.FirstOrDefault(sprite => sprite.name.Equals(id));
            if(sprite == null) { sprite = MissingTexture; }
            return sprite;
        }

        public static Item GetItemByID(string id) {
            var item = _itemDatabase[id];
            return item;
        }



        private void RegisterItems() {
            Register("air");
            Register("coins", 32);
            Register("test", 32);
            Register("test2");
            Register("test3");
            Register("test4", 6);

            RegisterLightSword("test_light_sword", 15f, 1);
            RegisterHeavySword("test_heavy_sword", 50f, 2);
            RegisterShield("test_shield", -0.75f, 3);

            RegisterEquipment("boots_of_the_traveler", EquipmentType.BOOTS,
                new StatModifier(1.5f, StatModifier.StatModifierType.ADDITIONAL, Stat.StatType.SPEED),
                new StatModifier(0.75f, StatModifier.StatModifierType.FLAT, Stat.StatType.DEFENCE));

            RegisterEquipment("test_equipment", EquipmentType.HELMET,
                new StatModifier(0.75f, StatModifier.StatModifierType.FLAT, Stat.StatType.DEFENCE));

            RegisterEquipment("test_chestplace", EquipmentType.CHESTPLATE,
                new StatModifier(0.75f, StatModifier.StatModifierType.FLAT, Stat.StatType.DEFENCE));
        }



        // NORMAL ITEM REGISTRATION

        private void Register(string id) {
            _itemDatabase.Add(id, new Item(id));
        }
        private void Register(string id, int maxStackSize) {
            _itemDatabase.Add(id, new Item(id, maxStackSize));
        }



        // EQUIPMENT REGISTRATION

        private void RegisterEquipment(string id, EquipmentType type, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new EquipmentItem(id, type, modifiers));
        }
        private void RegisterLightSword(string id, float damage, int weaponTier, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new LightSword(id, damage, weaponTier, modifiers));
        }
        private void RegisterHeavySword(string id, float damage, int weaponTier, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new HeavySword(id, damage, weaponTier, modifiers));
        }
        private void RegisterShield(string id, float slowDownEffect, int weaponTier, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new Shield(id, slowDownEffect, weaponTier, modifiers));
        }
    }
}