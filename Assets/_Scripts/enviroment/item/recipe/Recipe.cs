using Newtonsoft.Json;
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

        public Ingredient(ItemStack stack) : this(stack.Item, stack.StackSize) {
        }

        public Ingredient(Item item, int itemAmount = 1) {
            ItemID = item.ID;
            ItemAmount = itemAmount;
        }

        [JsonConstructor]
        public Ingredient(string itemID, int itemAmount = 1) {
            ItemID = itemID;
            ItemAmount = itemAmount;
        }

        public ItemStack GetIngredientStack() {
            return new ItemStack(ItemManager.GetItemByID(ItemID), ItemAmount);
        }
    }

    public abstract class RecipeObject<T> where T : Recipe {
        public Ingredient Result;
        public Ingredient[] Ingredients;

        public RecipeObject(Ingredient result, params Ingredient[] ingredients) {
            Result = result;
            Ingredients = ingredients;
        }

        public abstract T GetRecipe();
    }
}
