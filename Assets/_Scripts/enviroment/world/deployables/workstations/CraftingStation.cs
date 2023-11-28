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
    public class CraftingStation : Deployable {

        public Recipe LastRecipe { get; private set; }

        public ItemStack ResultStack;
        public Action RecipeTakenEvent;

        protected virtual void Awake() {
            StationInventory = new List<ItemStack>();
            for (int i = 0; i < 9; i++) {
                StationInventory.Add(new ItemStack(Items.AIR));
            }

            ResultStack = ItemStack.EMPTY;

            RecipeTakenEvent += Craft;
        }

        private void Update() {
            if (Input.GetKeyDown(KeyCode.F)) {
                Interact(GameObject.Find("Player").GetComponent<Player>());
            }
        }

        public override DeployableRenderer GetRenderer(Player interactor) {
            return new CraftingRenderer(interactor.Inventory, this, StationUIHolder.GetComponent<UIDocument>());
        }

        public virtual Recipe CheckForRecipes() {
            if(IsStationEmpty()) { // no idea why, but empty table has a recipe????
                return null;
            }
            var recipe = RecipeManager.FindRecipe(Recipe.RecipeType.SHAPELESS_CRAFTING, StationInventory.ToArray());
            if(recipe == null) {
                recipe = RecipeManager.FindRecipe(Recipe.RecipeType.SHAPED_CRAFTING, StationInventory.ToArray());
            }

            LastRecipe = recipe;
            return LastRecipe;
        }

        protected virtual void Craft() {
            var ingredients = new List<ItemStack>(LastRecipe.Ingredients);
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
