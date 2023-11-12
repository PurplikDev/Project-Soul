using System;
using System.Collections.Generic;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.enviroment.entity.player;
using roguelike.rendering.ui;
using roguelike.system.manager;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.world.deployable.workstation {
    public class CraftingStation : MonoBehaviour {

        public CraftingRenderer craftingRenderer;
        public List<ItemStack> StationInventory;
        public ItemStack ResultStack;

        public Action SlotUpdateEvent;
        public Action RecipeTakenEvent;

        private Recipe lastRecipe;

        protected virtual void Awake() {
            StationInventory = new List<ItemStack>();
            for (int i = 0; i < 9; i++) {
                StationInventory.Add(new ItemStack(Items.AIR));
            }

            ResultStack = ItemStack.EMPTY;

            SlotUpdateEvent += CheckForRecipes;
            RecipeTakenEvent += Craft;
        }

        protected virtual void CheckForRecipes() {

            if(IsStationEmpty()) { // no idea why, but empty table has a recipe????
                return;
            }

            var recipe = RecipeManager.FindRecipe(Recipe.RecipeType.SHAPELESS_CRAFTING, StationInventory.ToArray());
            if(recipe != null) { 
                
                lastRecipe = recipe;
                return;
            }

            recipe = RecipeManager.FindRecipe(Recipe.RecipeType.SHAPED_CRAFTING, StationInventory.ToArray());
            
            lastRecipe = recipe;
        }

        protected virtual void Craft() {

            if(lastRecipe == null) {
                return;
            }

            var ingredients = lastRecipe.Ingredients;
            foreach (ItemStack currentItem in StationInventory) {
                for (int i = ingredients.Count - 1; i > -1; i--) {
                    if (currentItem.Item == ingredients[i].Item && currentItem.StackSize >= ingredients[i].StackSize) {
                        currentItem.DecreaseStackSize(ingredients[i].StackSize);
                        ingredients.RemoveAt(i);
                    }
                }
            }
        }

        private bool IsStationEmpty() {
            foreach(ItemStack stack in StationInventory) {
                if(!stack.IsEmpty()) {
                    return false;
                }
            }
            return true;
        }
    }
}
