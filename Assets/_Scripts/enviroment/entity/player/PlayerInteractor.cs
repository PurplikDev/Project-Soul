using roguelike.enviroment.world.interactable;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = roguelike.system.input.PlayerInput;

namespace roguelike.enviroment.entity.player {
    public class PlayerInteractor {

        private Player _player;
        private PlayerInput _input;
        private Vector3 _mousePos;

        // todo: re-add logic for IHoverable ( OnHoverEnter, OnHover, OnHoverExit )

        public PlayerInteractor(Player player) {
            _player = player;
            _input = player.PlayerInput;

            _input.EnviromentControls.PrimaryAction.started += Interact;
        }

        public void Interact(InputAction.CallbackContext context) {
            
        }

        public void UpdateInteractor() {
            var (hit, hitPos) = GetAimPos();
            if(hit && Vector3.Distance(hitPos, _player.Position) < 2.5f)  {
                Debug.DrawLine(_player.Position, hitPos);
            }
        }

        private (bool hit, Vector3 hitPos) GetAimPos() {
            _mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(_mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Entity", "Object"))) {
                var hoverable = hit.transform.GetComponent<IHoverable>();
                if (hoverable != null) {
                    return (true, hit.transform.position);
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) {
                return (true, new Vector3(hit.point.x, 1, hit.point.z));
            }

            return (false, Vector3.zero);
        }
    }  
}