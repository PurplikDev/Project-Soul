using roguelike.enviroment.world.interactable;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = roguelike.system.input.PlayerInput;

namespace roguelike.enviroment.entity.player {
    public class PlayerInteractor {

        private Player _player;
        private PlayerInput _input;
        private Vector3 _mousePos, _aimPos;

        public Transform speeeen;

        private IHoverable _hoverable;

        // todo: re-add logic for IHoverable ( OnHoverEnter, OnHover, OnHoverExit )

        public PlayerInteractor(Player player) {
            _player = player;
            _input = player.PlayerInput;

            speeeen = GameObject.Find("speeeen").transform;

            _input.EnviromentControls.PrimaryAction.started += Interact;
        }

        public void Interact(InputAction.CallbackContext context) {
            Debug.DrawLine(_player.Position, _aimPos, Color.white, 5f);
        }

        public void UpdateInteractor() {
            var (hitPos, hoverable) = GetAimPos();
            _aimPos = hitPos;
            try {
                HandleHoverEvents(hitPos, hoverable);
            } catch(NullReferenceException) { /* :shrug: */}
        }

        private (Vector3 hitPos, IHoverable hoverable) GetAimPos() {
            _mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(_mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Entity", "Object"))) {
                var hoverable = hit.transform.GetComponent<IHoverable>();
                if (hoverable != null) {
                    return (hit.transform.position, hoverable);
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) {
                return (new Vector3(hit.point.x, 1, hit.point.z), null);
            }

            return (Vector3.zero, null);
        }
#nullable enable
        private void HandleHoverEvents(Vector3 hitPos, IHoverable? hoverable) {
            if(Vector3.Distance(hitPos, _player.Position) < 2.5f) {
                if(hoverable != null) {

                    if(hoverable == _hoverable) {
                        _hoverable.OnHover(_player);
                    } else if(hoverable != _hoverable) {
                        _hoverable?.OnHoverExit(_player);
                        _hoverable = hoverable;
                        _hoverable.OnHoverEnter(_player);
                    }
                } else {
                    _hoverable.OnHoverExit(_player);
                    _hoverable = null;
                }

            } else if(_hoverable != null) {
                _hoverable.OnHoverExit(_player);
                _hoverable = null;
            }
        }
#nullable disable
    }
}