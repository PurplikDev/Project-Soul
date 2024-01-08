using System.Collections.Generic;
using roguelike.core.eventsystem;
using UnityEngine;
using UnityEngine.UI;

namespace roguelike.environment.ui.hud {
    public class HealthBarRenderer : MonoBehaviour {

        public Transform HUDHeart;
        public Slider HUDBar;

        public HealthBarStyle BarStyle;

        private List<Heart> playerHearts = new List<Heart>();
        float oldMaxHealth = 0;

        private void Awake() {
            Events.PlayerHeathUpdateEvent += UpdateHealth;
        }

        private void UpdateHealth(PlayerHealthUpdateEvent healthUpdateEvent) {
            var maxHealth = healthUpdateEvent.Player.MaxHealth.Value;
            var health = healthUpdateEvent.Player.Health;
            maxHealth = Mathf.Ceil(maxHealth / 10);
            if (oldMaxHealth != maxHealth) {
                oldMaxHealth = maxHealth;

                foreach (var heart in playerHearts) { Destroy(heart.HeartImage.gameObject); }
                playerHearts.Clear();
                for (int i = 0; i < maxHealth; i++) {
                    var heart = Instantiate(Resources.Load("prefabs/ui/hud/Heart") as GameObject, HUDHeart);
                    playerHearts.Add(new Heart(heart.GetComponent<Image>()));
                }
            }

            foreach (var heart in playerHearts) {
                if (health >= 10) {
                    heart.UpdateHeart(10);
                } else if (health < 10 && health > 0) {
                    heart.UpdateHeart((int)health);
                } else {
                    heart.UpdateHeart(0);
                }
                health -= 10;
            }
        }

        public enum HealthBarStyle {
            CLASSIC,
            BAR,
            TEXT,
            CLASSIC_WITH_TEXT
        }
    }

    public class Heart {
        public Image HeartImage { get; private set; }

        public Heart(Image heartImage) {
            HeartImage = heartImage;
            UpdateHeart(0);
        }

        public void UpdateHeart(int value) {
            HeartImage.sprite = Resources.Load<Sprite>($"sprites/ui/hud/heart{value}");
        }
    }
}