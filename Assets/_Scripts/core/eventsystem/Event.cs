using roguelike.enviroment.entity.player;

namespace roguelike.core.eventsystem {
    public abstract class Event { }

    public abstract class PlayerEvent : Event {
        public Player Player { get; private set; }
        public PlayerEvent(Player player) {
            Player = player;
        }
    }

    public class PlayerHealthUpdateEvent : PlayerEvent
        { public PlayerHealthUpdateEvent(Player player) : base(player) { } }

    public class PlayerMaxHealthUpdateEvent : PlayerEvent
        { public PlayerMaxHealthUpdateEvent(Player player) : base(player) { } }
}