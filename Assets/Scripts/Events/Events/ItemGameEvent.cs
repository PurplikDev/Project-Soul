using UnityEngine;
using io.purplik.ProjectSoul.InventorySystem;

namespace io.purplik.ProjectSoul.EventSystem
{
    [CreateAssetMenu(fileName = "New Item Event", menuName = "Events/Item Event")]
    public class ItemGameEvent : BaseGameEvent<Item> { }
}