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

        public Transform speeeen;

        private IHoverable _hoverable;

        // todo: fix the aiming sprite, it isn't bright enough and blends in too much
        // todo part 2: proper aiming gameobject

        public PlayerInteractor(Player player) {
            _player = player;
            _input = player.PlayerInput;

            // temp object for testing
            speeeen = GameObject.Find("speeeen").transform;

            _input.EnviromentControls.PrimaryAction.started += Attack;
            _input.EnviromentControls.SecondaryAction.started += Interact;
        }

        public void Attack(InputAction.CallbackContext context) {
            _player.Attack();
        }
        public void Interact(InputAction.CallbackContext context) {
            _hoverable?.Interact(_player);
        }

        public void UpdateInteractor() {
            var (hit, pos, hoverable) = GetAimPos();
            if(hit) {
                var aimPos = pos - _player.Position;
                aimPos.y = 1;
                Debug.DrawLine(_player.Position, pos, Color.white);
                speeeen.rotation = new Quaternion(0, Quaternion.LookRotation(aimPos).y, 0, Quaternion.LookRotation(aimPos).w);

                if(hoverable != null) {
                    HandleHoverEvents(pos, hoverable);
                }
            }
            
        }

        private (bool hit, Vector3 hitPos, IHoverable) GetAimPos() {
            _mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(_mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Entity", "Object"))) {
                var hoverable = hit.transform.GetComponent<IHoverable>();
                if (hoverable != null) {
                    return (true, hit.transform.position, hoverable);
                } else {
                    return (true, hit.transform.position, null);
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"))) {
                HoverExit();
                return (true, new Vector3(hit.point.x, 1, hit.point.z), null);
            }

            return (false, Vector3.zero, null);
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
                } else { HoverExit(); }
            } else if(_hoverable != null) { HoverExit(); }
        }
#nullable disable

        private void HoverExit() {
            _hoverable?.OnHoverExit(_player);
            _hoverable = null;
        }
    }
}