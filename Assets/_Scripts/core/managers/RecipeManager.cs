using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.system.singleton;
using UnityEngine;
using static roguelike.core.item.recipe.Recipe;

namespace roguelike.system.manager {
    public class RecipeManager : PersistentSingleton<RecipeManager> {

        private static Dictionary<RecipeType, List<Recipe>> _recipeDatabase = new Dictionary<RecipeType, List<Recipe>>();

        public List<Recipe> ShapelessRecipes = new List<Recipe>();
        public List<Recipe> ShapedRecipes = new List<Recipe>();

        protected override void Awake() {
            base.Awake();
            RegisterRecipes();
        }

        public static Recipe FindRecipe(RecipeType type, ItemStack[] input) {
            foreach(KeyValuePair<RecipeType, List<Recipe>> entry in _recipeDatabase) {
                if(entry.Key != type) { continue; }
                foreach(Recipe recipe in entry.Value) {
                    if(recipe.CheckRecipe(input)) { return recipe; }
                }
            }
            return null;
        }

        /// <summary>
        /// Method for registering recipes from a json file.
        /// </summary>
        private void RegisterShapeless(ShapelessRecipeObject recipeObject, List<Recipe> recipeList) {
            recipeList.Add(recipeObject.GetRecipe());
        }

        /// <summary>
        /// Method for registering recipes from a json file.
        /// </summary>
        private void RegisterShaped(ShapedRecipeObject recipeObject, List<Recipe> recipeList) {
            recipeList.Add(recipeObject.GetRecipe());
        }



        private void RegisterRecipes() {
            try {
                var shapelessRecipes = Resources.LoadAll<TextAsset>("data/recipes/shapeless");
                foreach (var recipe in shapelessRecipes) { RegisterShapeless(JsonConvert.DeserializeObject<ShapelessRecipeObject>(recipe.text.ToString()), ShapelessRecipes); }
                _recipeDatabase.Add(RecipeType.SHAPELESS_CRAFTING, ShapelessRecipes);

                var shapedRecipes = Resources.LoadAll<TextAsset>("data/recipes/shaped");
                foreach (var recipe in shapedRecipes) { RegisterShaped(JsonConvert.DeserializeObject<ShapedRecipeObject>(recipe.text.ToString()), ShapedRecipes); }
                _recipeDatabase.Add(RecipeType.SHAPED_CRAFTING, ShapedRecipes);
            } catch(ArgumentException) {
                Debug.LogWarning("Recipes already present!");
            }
        }
    }
}