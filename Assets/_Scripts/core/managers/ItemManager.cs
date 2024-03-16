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

            // Light Weapon Registry

            RegisterLightSword("crooked_blade", 32, 12, 0.5f, 0.75f, 1, 
                new StatModifier(1.125f, StatModifierType.FLAT, StatType.SPEED),
                new StatModifier(2f, StatModifierType.FLAT, StatType.TEMPLAR),
                new StatModifier(1.5f, StatModifierType.FLAT, StatType.ROGUE));



            // Trinket Registry

            RegisterTrinket("amulet_of_beautiful_eyes", 526,
                new StatModifier(1.5f, StatModifierType.ADDITIONAL, StatType.HEALTH));



            // Armor Registry

            // Helmets

            RegisterEquipment("hood_of_the_traveler", 64, EquipmentType.HELMET,
                new StatModifier(1.25f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE));

            // Chestplates

            RegisterEquipment("tunic_of_the_traveler", 128, EquipmentType.CHESTPLATE,
                new StatModifier(1.25f, StatModifierType.FLAT, StatType.HEALTH),
                new StatModifier(1.5f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE));

            // Leggings

            RegisterEquipment("leggings_of_the_traveler", 96, EquipmentType.PANTS,
                new StatModifier(1.25f, StatModifierType.FLAT, StatType.HEALTH),
                new StatModifier(1.5f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE));

            // Boots

            RegisterEquipment("boots_of_the_traveler", 256, EquipmentType.BOOTS,
                new StatModifier(1.75f, StatModifierType.ADDITIONAL, StatType.SPEED),
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