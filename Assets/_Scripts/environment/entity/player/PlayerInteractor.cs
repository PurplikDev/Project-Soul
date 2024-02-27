using roguelike.environment.world.interactable;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using PlayerInput = roguelike.system.input.PlayerInput;

namespace roguelike.environment.entity.player {
    public class PlayerInteractor : MonoBehaviour {

        [HideInInspector]
        public Player Player;
        private PlayerInput _input;
        private Vector3 _mousePos;

        private IHoverable _hoverable;

        // todo: fix the aiming sprite, it isn't bright enough and blends in too much

        public void Start() {
            _input = Player.PlayerInput;

            // access the game object in a different way later
            _input.EnviromentControls.PrimaryAction.started += ActionPrimary;
            _input.EnviromentControls.SecondaryAction.started += ActionSecondary;
            _input.EnviromentControls.InteractionAction.started += Interact;
        }

        private void Update() {
            UpdateInteractor();
        }

        public void ActionPrimary(InputAction.CallbackContext context) { }
            // { Debug.LogWarning("Hook this up to the state machine so that it's better ya know"); }

        public void ActionSecondary(InputAction.CallbackContext context)
            { Player.SecondaryAction(); }

        public void Interact(InputAction.CallbackContext context)
            { _hoverable?.Interact(Player); }



        public void UpdateInteractor() {
            var (hit, pos, hoverable) = GetAimPos();
            if(hit) {
                var aimPos = pos - Player.Position;
                aimPos.y = 1;
                transform.rotation = new Quaternion(0, Quaternion.LookRotation(aimPos).y, 0, Quaternion.LookRotation(aimPos).w);
                Player.LookDirection = transform.forward;

                if(hoverable != null) {
                    HandleHoverEvents(pos, hoverable);
                }
            }
            
        }

        private (bool hit, Vector3 hitPos, IHoverable) GetAimPos() {
            _mousePos = Mouse.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(_mousePos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Entity", "Object"), QueryTriggerInteraction.Ignore)) {
                var hoverable = hit.transform.GetComponent<IHoverable>();
                if (hoverable != null) {
                    return (true, hit.transform.position, hoverable);
                } else {
                    return (true, hit.transform.position, null);
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Ground"), QueryTriggerInteraction.Ignore)) {
                HoverExit();
                return (true, new Vector3(hit.point.x, 1, hit.point.z), null);
            }

            return (false, Vector3.zero, null);
        }
#nullable enable
        private void HandleHoverEvents(Vector3 hitPos, IHoverable? hoverable) {
            if(Vector3.Distance(hitPos, Player.Position) < 2.5f) {
                if(hoverable != null) {
                    if(hoverable == _hoverable) {
                        _hoverable.OnHover(Player);
                    } else if(hoverable != _hoverable) {
                        _hoverable?.OnHoverExit(Player);
                        _hoverable = hoverable;
                        _hoverable.OnHoverEnter(Player);
                    }
                } else { HoverExit(); }
            } else if(_hoverable != null) { HoverExit(); }
        }
#nullable disable

        private void HoverExit() {
            _hoverable?.OnHoverExit(Player);
            _hoverable = null;
        }
    }
}