using System.Collections.Generic;

namespace Trivia.WebApi
{
    internal class JsonEventDispatcher : IDispatchEvent
    {
        public List<object> Events { get; private set; }

        public JsonEventDispatcher()
        {
            Events = new List<object>();
        }

        public void Display(string message)
        {
            Events.Add(message);
        }

        public void Dispatch<TEvent>(TEvent @event)
        {
            Events.Add(@event);
        }

        public void Flush()
        {
            Events = new List<object>();
        }
    }
}