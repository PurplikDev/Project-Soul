using System.Collections.Generic;

namespace roguelike.core.item.recipe { 
    public abstract class Recipe {

        public List<ItemStack> Ingredients { get; private set; }
        public ItemStack Result { get; private set; }

        public Recipe(List<ItemStack> ingredients, ItemStack result) {
            Ingredients = ingredients;
            Result = result;
        }

        public abstract bool CheckRecipe(params ItemStack[] inputItems);

        public enum RecipeType {
            SHAPED_CRAFTING,
            SHAPELESS_CRAFTING
        }
    }
}
