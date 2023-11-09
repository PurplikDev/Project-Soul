using System.Collections.Generic;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.system.singleton;

using static roguelike.core.item.recipe.Recipe;

namespace roguelike.system.manager {
    public class RecipeManager : PersistentSingleton<RecipeManager> {

        private static Dictionary<RecipeType, Recipe> _recipeDatabase = new Dictionary<RecipeType, Recipe>();

        protected override void Awake() {
            base.Awake();
            RegisterRecipes();
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