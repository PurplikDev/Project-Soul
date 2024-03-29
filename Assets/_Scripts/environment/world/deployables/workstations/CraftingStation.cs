using System;
using System.Collections.Generic;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.environment.entity.player;
using roguelike.rendering.ui;
using roguelike.system.manager;
using UnityEngine.UIElements;

namespace roguelike.environment.world.deployable.workstation {
    public class CraftingStation : Deployable {

        public Recipe LastRecipe { get; private set; }

        public ItemStack ResultStack;
        public Action RecipeTakenEvent;

        protected virtual void Awake() {
            StationInventory = new List<ItemStack>();
            for (int i = 0; i < 9; i++) {
                StationInventory.Add(ItemStack.EMPTY);
            }

            ResultStack = ItemStack.EMPTY;

            RecipeTakenEvent += Craft;
        }

        public override DeployableRenderer GetRenderer(Player interactor) {
            return new CraftingRenderer(interactor.Inventory, this, StationUIHolder.GetComponent<UIDocument>());
        }

        public virtual Recipe CheckForRecipes() {
            var recipe = RecipeManager.FindRecipe(Recipe.RecipeType.SHAPED_CRAFTING, StationInventory.ToArray());
            if(recipe == null) {
                recipe = RecipeManager.FindRecipe(Recipe.RecipeType.SHAPELESS_CRAFTING, StationInventory.ToArray());
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
    }
}
