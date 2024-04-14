using roguelike.core.item.recipe;
using roguelike.core.utils.mathematicus;
using roguelike.system.manager;
using UnityEngine;

namespace roguelike.environment.world.interactable {
    public class RecipePrefab : MonoBehaviour {

        [SerializeField] SpriteRenderer[] displaySlots;
        [SerializeField] SpriteRenderer displayResult;

        public void Start() {
            Recipe recipe;

            if(Mathematicus.ChanceIn(1, 2)) {
                recipe = RecipeManager.Instance.ShapedRecipes[Random.Range(0, RecipeManager.Instance.ShapedRecipes.Count)];
            } else {
                recipe = RecipeManager.Instance.ShapelessRecipes[Random.Range(0, RecipeManager.Instance.ShapelessRecipes.Count)];
            }

            if(recipe == null) {
                Debug.Log("bruh");
                return;
            }

            try {
                for (int i = 0; i < displaySlots.Length; i++) {
                    displaySlots[i].sprite = recipe.Ingredients[i].Item.Icon;
                }
            } catch(System.ArgumentOutOfRangeException) {
                Debug.Log("it is what it is");
            } finally {
                displayResult.sprite = recipe.Result.Item.Icon;
            }
        }
    }
}