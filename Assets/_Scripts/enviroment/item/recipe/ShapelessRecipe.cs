using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace roguelike.core.item.recipe {
    public class ShapelessRecipe : Recipe {

        public ShapelessRecipe(List<ItemStack> ingredients, ItemStack result)
            : base(ingredients, result) {}

        public override bool CheckRecipe(params ItemStack[] inputItems) {
            bool isValid = false;
            var input = inputItems.ToList();

            int actualLength = 0;

            foreach (ItemStack item in inputItems) { 
                if(!item.IsEmpty()) { actualLength++; }
            }


            if(actualLength != Ingredients.Count) {
                return false;
            }

            foreach(ItemStack currentItem in Ingredients) {
                for(int i = input.Count - 1; i > -1; i--) {
                    if(currentItem.Item == input[i].Item && currentItem.StackSize <= input[i].StackSize) {
                        input.RemoveAt(i);
                        isValid = true;
                        break;
                    } else {
                        isValid = false;
                    }
                }
                if(!isValid) {
                    return false;
                }
            }
            return true;
        }
    }
}