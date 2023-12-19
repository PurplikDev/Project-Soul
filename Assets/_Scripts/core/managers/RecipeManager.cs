using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.system.singleton;
using UnityEngine;
using static roguelike.core.item.recipe.Recipe;

namespace roguelike.system.manager {
    public class RecipeManager : PersistentSingleton<RecipeManager> {

        private static Dictionary<RecipeType, List<Recipe>> _recipeDatabase = new Dictionary<RecipeType, List<Recipe>>();

        private List<Recipe> _shapelessRecipes = new List<Recipe>();
        private List<Recipe> _shapedRecipes = new List<Recipe>();

        protected override void Awake() {
            base.Awake();
            RegisterRecipes();
        }

        public static Recipe FindRecipe(RecipeType type, ItemStack[] input) {
            foreach (KeyValuePair<RecipeType, List<Recipe>> entry in _recipeDatabase) {
                foreach (Recipe recipe in entry.Value) {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine("Result: " + recipe.Result);
                    foreach(ItemStack stack in recipe.Ingredients) {
                        builder.AppendLine(stack.ToString());
                    }
                    Debug.Log(builder.ToString());
                }
            }

            foreach(KeyValuePair<RecipeType, List<Recipe>> entry in _recipeDatabase) {
                if(entry.Key != type) { continue; }
                foreach(Recipe recipe in entry.Value) {
                    if(recipe.CheckRecipe(input)) { return recipe; }
                }
            }
            Debug.Log("didn't find recipe");
            return null;
        }

        // todo: fix registration to not be stupid

        /// <summary>
        /// Method for registering recipes in the code.
        /// </summary>
        // private Recipe RegisterShapeless(ItemStack result, params ItemStack[] input) { }

        /// <summary>
        /// Method for registering recipes from a json file.
        /// </summary>
        private void RegisterShapeless(ShapelessRecipeObject recipeObject, List<Recipe> recipeList) {
            recipeList.Add(recipeObject.GetRecipe());
        }

        private void RegisterShaped(ShapedRecipeObject recipeObject, List<Recipe> recipeList) {
            recipeList.Add(recipeObject.GetRecipe());
        }



        private void RegisterRecipes() {

            // todo: replace place holder testing recipes with proper recipes :3

            // SHAPELESS REGISTRATION

            var shapelessRecipes = Resources.LoadAll<TextAsset>("data/recipes/shapeless");
            foreach (var recipe in shapelessRecipes) { RegisterShapeless(JsonConvert.DeserializeObject<ShapelessRecipeObject>(recipe.text.ToString()), _shapelessRecipes); }
            _recipeDatabase.Add(RecipeType.SHAPELESS_CRAFTING, _shapelessRecipes);

            var shapedRecipes = Resources.LoadAll<TextAsset>("data/recipes/shaped");
            foreach (var recipe in shapedRecipes) { RegisterShaped(JsonConvert.DeserializeObject<ShapedRecipeObject>(recipe.text.ToString()), _shapedRecipes); }
            _recipeDatabase.Add(RecipeType.SHAPED_CRAFTING, _shapedRecipes);


            // SHAPED REGISTRATION

        }
    }
}