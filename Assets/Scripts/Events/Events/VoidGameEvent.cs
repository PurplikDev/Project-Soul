using UnityEngine;

namespace io.purplik.ProjectSoul.EventSystem
{
    [CreateAssetMenu(fileName = "New Void Event", menuName = "Events/Void Event")]
    public class VoidGameEvent : BaseGameEvent<Void>
    {
        public void Raise() => Raise(new Void());
    }
}