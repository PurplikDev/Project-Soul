using Newtonsoft.Json;
using roguelike.core.item;
using roguelike.core.item.recipe;
using roguelike.enviroment.world;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = roguelike.system.input.PlayerInput;

namespace roguelike.enviroment.entity.player {
    public class PlayerInteractor {

        private Player _player;
        private PlayerInput _input;
        private Vector2 _mousePos;
        private Transform _hoveredTransform;
        private IInteractable _hoveredInteractable;

        public PlayerInteractor(Player player) {
            _player = player;
            _input = player.PlayerInput;

            _input.EnviromentControls.PrimaryAction.started += Interact;
        }

        public void UpdateInteractor() {
            _mousePos = Mouse.current.position.ReadValue();
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(_mousePos);
            if(Physics.Raycast(ray, out hit)) {
                _hoveredTransform = hit.transform;
                _hoveredInteractable = _hoveredTransform.GetComponent<IInteractable>();
            }
        }

        public void Interact(InputAction.CallbackContext context) {
            if(_hoveredInteractable != null && Vector3.Distance(_hoveredTransform.position, _player.Position) <= 2.5f) {
                _hoveredInteractable.Interact(_player);
            }

            string json = JsonConvert.SerializeObject(new RecipeObject(Recipe.RecipeType.SHAPELESS_CRAFTING,
                new Ingredient(Items.TEST, 4), new Ingredient(Items.TEST2), new Ingredient(Items.TEST4, 2)
                ), Formatting.Indented);
            Debug.Log(json);
        }
    }  
}