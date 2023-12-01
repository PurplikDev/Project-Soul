using System.Collections.Generic;
using roguelike.core.utils;
using UnityEngine;

namespace roguelike.core.item.recipe {
    public class ShapedRecipe : Recipe {

        public ShapedRecipe(List<ItemStack> ingredients, ItemStack result)
            : base(ingredients, result) {
            if(ingredients.Count != 9)
                { Debug.LogError(GlobalStaticValues.SHAPED_RECIPE_SIZE_ERROR); }
        }

        public override bool CheckRecipe(params ItemStack[] inputItems) {
            if(inputItems.Length != Ingredients.Count) {
                return false;
            }

            for(int i = 0; i < Ingredients.Count; i++) {
                if(Ingredients[i].Item != inputItems[i].Item ||
                    Ingredients[i].StackSize > inputItems[i].StackSize) {
                    return false;
                }
            }
            return true;
        }
    }

    public class ShapedRecipeObject : RecipeObject<ShapedRecipe> {
        public ShapedRecipeObject(Ingredient result, params Ingredient[] ingredients) : base(result, ingredients) { }

        public override ShapedRecipe GetRecipe() {
            List<ItemStack> inputItems = new List<ItemStack>();
            foreach (Ingredient ingredient in Ingredients) {
                inputItems.Add(ingredient.GetIngredientStack());
            }
            return new ShapedRecipe(inputItems, Result.GetIngredientStack());
        }
    }
}