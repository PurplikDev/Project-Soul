using roguelike.environment.entity.statsystem;
using roguelike.system.singleton;
using System;
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
            try { // todo: find a better way to do this
                RegisterItems();
                MissingTexture = Resources.Load<Sprite>("sprites/missing_texture");
                ItemIcons = Resources.LoadAll<Sprite>("sprites/items").ToList();
            } catch(ArgumentException) {
                Debug.LogWarning("Items already present");
            }
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


        // i just want to say, i hate this
        // i have no idea why i made this like this
        // no idea what i was thinking back then
        // i want to rewrite it, but that would take too much effort

        private void RegisterItems() {
            Register("air", 0);
            Register("coins", 32, 1);
            Register("test", 32, 2);
            Register("test2", 16);
            Register("test3", 32);
            Register("test4", 6, 5);

            RegisterTrophy("test_trophy", 64);

            RegisterTrinket("amulet_of_beautiful_eyes", 526,
                new StatModifier(1.5f, StatModifierType.ADDITIONAL, StatType.HEALTH));

            RegisterLightSword("test_light_sword", 64, 15f, 0.25f, 0.5f, 1);
            RegisterHeavySword("test_heavy_sword", 128, 50f, 0.75f, 1.5f, 2);
            RegisterShield("test_shield", 64, -0.75f, 1, 3);

            RegisterEquipment("boots_of_the_traveler", 256, EquipmentType.BOOTS,
                new StatModifier(1.5f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE));

            RegisterEquipment("test_equipment", 16, EquipmentType.HELMET,
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE));

            RegisterEquipment("test_chestplace", 32, EquipmentType.CHESTPLATE,
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE));
        }



        // NORMAL ITEM REGISTRATION

        private void Register(string id, int itemValue) {
            _itemDatabase.Add(id, new Item(id, itemValue));
        }
        private void Register(string id, int maxStackSize, int itemValue) {
            _itemDatabase.Add(id, new Item(id, maxStackSize, itemValue));
        }



        private void RegisterTrophy(string id, int itemValue) {
            _itemDatabase.Add(id, new Trophy(id, itemValue));
        }


        // EQUIPMENT REGISTRATION

        private void RegisterEquipment(string id, int value, EquipmentType type, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new EquipmentItem(id, value, type, modifiers));
        }
        private void RegisterTrinket(string id, int value, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new EquipmentItem(id, value, EquipmentType.TRINKET, modifiers));
        }

        // WEAPON AND SHIELD REGISTRATION
        private void RegisterLightSword(string id, int value, float damage, float swingSpeed, float attackCooldown, int weaponTier, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new LightSword(id, value, damage, swingSpeed, attackCooldown, weaponTier, modifiers));
        }
        private void RegisterHeavySword(string id, int value, float damage, float swingSpeed, float attackCooldown, int weaponTier, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new HeavySword(id, value, damage, swingSpeed, attackCooldown, weaponTier, modifiers));
        }
        private void RegisterShield(string id, int value, float slowDownEffect, int weaponTier, int maxAmountOfBlocks, params StatModifier[] modifiers) {
            _itemDatabase.Add(id, new Shield(id, value, slowDownEffect, weaponTier, maxAmountOfBlocks, modifiers));
        }
    }
}