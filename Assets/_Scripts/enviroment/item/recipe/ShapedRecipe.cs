using System.Collections.Generic;
using UnityEngine;

namespace roguelike.core.item.recipe {
    public class ShapedRecipe : Recipe {

        public ShapedRecipe(List<ItemStack> ingredients, ItemStack result)
            : base(ingredients, result) {
            if(ingredients.Count != 9)
                { Debug.LogError("Shaped recipe isn't the correct size!"); }
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
}