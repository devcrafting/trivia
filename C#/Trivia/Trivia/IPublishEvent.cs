namespace Trivia
{
    public interface IPublishEvent
    {
        void Publish<TEvent>(TEvent @event);
    }
}