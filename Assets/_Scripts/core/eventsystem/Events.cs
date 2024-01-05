using System;

namespace roguelike.core.eventsystem
{
    public class Events
    {
        // some big global events that might be useful
        public static Action<PlayerHealthUpdateEvent> PlayerHeathUpdateEvent;
    }
}