using System.Collections.Generic;
using static roguelike.core.item.recipe.Recipe;

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

    public class Ingredient {
        public string ItemID { get; private set; }
        public int ItemAmount { get; private set; }

        public Ingredient(ItemStack stack) {
            ItemID = stack.Item.ID;
            ItemAmount = stack.StackSize;
        }

        public Ingredient(Item item, int itemAmount = 1) {
            ItemID = item.ID;
            ItemAmount = itemAmount;
        }

        public ItemStack GetIngredientStack() {
            return new ItemStack(ItemManager.GetItemByID(ItemID), ItemAmount);
        }
    }

    public class RecipeObject {
        public RecipeType RecipeType;
        public Ingredient Result;
        public Ingredient[] Ingredients;

        public RecipeObject(RecipeType recipeType, Ingredient result, params Ingredient[] ingredients) {
            Result = result;
            Ingredients = ingredients;
            RecipeType = recipeType;
        }
    }
}
