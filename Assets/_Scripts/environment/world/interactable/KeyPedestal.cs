using roguelike.core.item;
using roguelike.environment.entity.player;
using UnityEngine;

namespace roguelike.environment.world.interactable {
    public class KeyPedestal : MonoBehaviour, IHoverable {

        [SerializeField] GameObject keyDisplay;

        public void Interact(Player player) {
            if (player.Inventory.HasSpace()) {
                player.Inventory.AddItem(new ItemStack(ItemManager.GetItemByID("ancient_gate_key"), 1));
                player.DisplayMessage("ui.key_got");
                Destroy(keyDisplay);
            } else {
                player.DisplayMessage("ui.no_space");
            }
        }

        public void OnHover(Player player) {}
        public void OnHoverEnter(Player player) {}
        public void OnHoverExit(Player player) {}
    }
}