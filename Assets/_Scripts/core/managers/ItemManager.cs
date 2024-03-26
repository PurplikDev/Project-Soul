using roguelike.environment.entity.statsystem;
using roguelike.environment.entity.statuseffect;
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
            
            // Generic Item/Registry

            Register("air", 0);
            Register("coins", 1280, 1);

            // Crafting Materials
            Register("magic_silk", 32, 32);
            Register("old_bone", 32, 8);
            Register("mystic_bone", 16, 48);
            Register("piece_of_chainmail", 8, 16);
            Register("chainmail", 16, 32);

            // UseItems
            RegisterUseItem("bandage", 8, UseItemType.HEALING, 5);

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
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE),
                new StatModifier(1f, StatModifierType.FLAT, StatType.ROGUE),
                new StatModifier(1.75f, StatModifierType.FLAT, StatType.THAUMATURGE));

            RegisterEquipment("paladin_helmet", 64, EquipmentType.HELMET,
                new StatModifier(0.75f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(1.25f, StatModifierType.FLAT, StatType.DEFENCE),
                new StatModifier(1f, StatModifierType.FLAT, StatType.TEMPLAR),
                new StatModifier(1.75f, StatModifierType.FLAT, StatType.THAUMATURGE));

            // Chestplates

            RegisterEquipment("tunic_of_the_traveler", 128, EquipmentType.CHESTPLATE,
                new StatModifier(5f, StatModifierType.FLAT, StatType.HEALTH),
                new StatModifier(1.5f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE),
                new StatModifier(1f, StatModifierType.FLAT, StatType.ROGUE),
                new StatModifier(1.75f, StatModifierType.FLAT, StatType.THAUMATURGE));

            // Leggings

            RegisterEquipment("leggings_of_the_traveler", 96, EquipmentType.PANTS,
                new StatModifier(1.25f, StatModifierType.FLAT, StatType.HEALTH),
                new StatModifier(1.5f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE),
                new StatModifier(1f, StatModifierType.FLAT, StatType.ROGUE),
                new StatModifier(1.75f, StatModifierType.FLAT, StatType.THAUMATURGE));

            // Boots

            RegisterEquipment("boots_of_the_traveler", 256, EquipmentType.BOOTS,
                new StatModifier(1.75f, StatModifierType.ADDITIONAL, StatType.SPEED),
                new StatModifier(0.75f, StatModifierType.FLAT, StatType.DEFENCE),
                new StatModifier(1f, StatModifierType.FLAT, StatType.ROGUE),
                new StatModifier(1.75f, StatModifierType.FLAT, StatType.THAUMATURGE));



            // Debug/Admin/Easter Egg Items

            Register("missing_item", int.MaxValue);
            RegisterUseItem("missing_use_item", int.MaxValue, UseItemType.HEALING, float.MaxValue);
            RegisterLightSword("missing_light_sword", int.MaxValue, int.MaxValue, 0f, 0f, int.MaxValue, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
            RegisterHeavySword("missing_heavy_sword", int.MaxValue, int.MaxValue, 0f, 0f, int.MaxValue, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
            RegisterShield("missing_shield", int.MaxValue, 0f, int.MaxValue, int.MaxValue, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
            RegisterEquipment("missing_cap", int.MaxValue, EquipmentType.HELMET, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
            RegisterEquipment("missing_tshirt", int.MaxValue, EquipmentType.CHESTPLATE, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
            RegisterEquipment("missing_shorts", int.MaxValue, EquipmentType.PANTS, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
            RegisterEquipment("missing_crocs", int.MaxValue, EquipmentType.BOOTS, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
            RegisterTrinket("missing_trinket", int.MaxValue, new StatModifier(10f, StatModifierType.FLAT, StatType.HEALTH), new StatModifier(10f, StatModifierType.FLAT, StatType.DEFENCE), new StatModifier(10f, StatModifierType.FLAT, StatType.SPEED), new StatModifier(10f, StatModifierType.FLAT, StatType.TEMPLAR), new StatModifier(10f, StatModifierType.FLAT, StatType.ROGUE), new StatModifier(10f, StatModifierType.FLAT, StatType.THAUMATURGE));
        }



        // NORMAL ITEM REGISTRATION

        private void Register(string id, int itemValue) {
            _itemDatabase.Add(id, new Item(id, itemValue));
        }
        private void Register(string id, int maxStackSize, int itemValue) {
            _itemDatabase.Add(id, new Item(id, maxStackSize, itemValue));
        }

        private void RegisterUseItem(string id, int value, UseItemType type, float potency) {
            _itemDatabase.Add(id, new UseItem(id, value, type, potency));
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