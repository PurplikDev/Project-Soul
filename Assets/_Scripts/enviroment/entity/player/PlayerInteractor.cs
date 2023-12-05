using roguelike.enviroment.world.interactable;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = roguelike.system.input.PlayerInput;

namespace roguelike.enviroment.entity.player {
    public class PlayerInteractor {

        private Player _player;
        private PlayerInput _input;
        private Vector2 _mousePos;
        private Transform _hoveredTransform;
        private Interactable _hoveredInteractable;

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
                var newTransform = hit.transform;

                if(newTransform != null) {
                    var newInteractable = newTransform.GetComponent<Interactable>();
                    if(newTransform != _hoveredTransform) {
                        _hoveredInteractable?.OnHoverExit(_player);
                        _hoveredTransform = newTransform;
                        _hoveredInteractable = newInteractable;
                        _hoveredInteractable?.OnHoverEnter(_player);
                    } else {
                        _hoveredInteractable?.OnHover(_player);
                    }
                } 
            }
        }

        public void Interact(InputAction.CallbackContext context) {
            if(_hoveredInteractable != null && Vector3.Distance(_hoveredTransform.position, _player.Position) <= 2.5f) {
                _hoveredInteractable.Interact(_player);
            }
        }
    }  
}