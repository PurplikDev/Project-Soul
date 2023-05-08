namespace io.purplik.ProjectSoul.EventSystem
{
    public interface IGameEventListener<T>
    {
        void OnEventRaised(T item);
    }
}