using System.Collections.Generic;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.system.singleton;
using UnityEngine;
using static roguelike.core.item.recipe.Recipe;

namespace roguelike.system.manager {
    public class RecipeManager : PersistentSingleton<RecipeManager> {

        private static Dictionary<RecipeType, Recipe> _recipeDatabase = new Dictionary<RecipeType, Recipe>();

        protected override void Awake() {
            base.Awake();
            RegisterRecipes();
        }



        public Recipe FindRecipe(RecipeType type, ItemStack[] input) {
            foreach(KeyValuePair<RecipeType, Recipe> entry in _recipeDatabase) {
                if(entry.Key != type) { continue; }
                if(entry.Value.CheckRecipe(input)) { return entry.Value; }
            }
            Debug.LogWarning("No" + type.ToString() + "recipe found with inputs:" + input);
            return null;
        }


        private void RegisterShapeless(List<ItemStack> ingredients) {
            _recipeDatabase.Add(RecipeType.SHAPELESS_CRAFTING, new ShapelessRecipe(ingredients, new ItemStack(Items.TEST_EQUIPMENT)));
        }

        private void RegisterShaped(List<ItemStack> ingredients) {
            _recipeDatabase.Add(RecipeType.SHAPED_CRAFTING, new ShapedRecipe(ingredients, new ItemStack(Items.TEST_EQUIPMENT)));
        }



        private void RegisterRecipes() {

            // todo: replace place holder testing recipes with proper recipes :3

            // SHAPELESS REGISTRATION

            RegisterShapeless(new List<ItemStack> {
                new ItemStack(Items.TEST, 4),
                new ItemStack(Items.TEST2),
                new ItemStack(Items.TEST3),
                new ItemStack(Items.TEST4),
            });



            // SHAPED REGISTRATION

            RegisterShaped(new List<ItemStack> {
                new ItemStack(Items.TEST, 4),
                new ItemStack(Items.TEST2),
                new ItemStack(Items.TEST3),
                new ItemStack(Items.TEST4),
            });
        }
    }
}