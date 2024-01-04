using System.Collections.Generic;
using roguelike.core.eventsystem;
using UnityEngine;
using UnityEngine.UIElements;

namespace roguelike.enviroment.ui.hud {
    public class HUDRenderer : MonoBehaviour {
        public UIDocument HUDDocument { get; private set; }

        private List<HUDHeart> hearts;

        private void Awake() {
            HUDDocument = GetComponent<UIDocument>();

            hearts = HUDDocument.rootVisualElement.

            Events.PlayerMaxHealthUpdateEvent += context => {
                UpdateMaxHealth();
                UpdateHealth();
            };

            Events.PlayerHeathUpdateEvent += context => { UpdateHealth(); };
        }

        private void UpdateHealth() {

        }

        private void UpdateMaxHealth() {

        }
    }
}