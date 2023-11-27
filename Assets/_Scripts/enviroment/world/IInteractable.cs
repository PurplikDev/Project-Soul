using roguelike.enviroment.entity.player;

namespace roguelike.enviroment.world {
    public interface IInteractable {
        public void Interact(Player player);
    }
}