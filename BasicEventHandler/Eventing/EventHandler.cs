//   Written by Harry Kerr

namespace BasicEventHandler.Eventing
{

    /// <summary>
    /// Used to annotate other functions
    /// </summary>
    public class EventHandler : System.Attribute
    {
        /// <summary>
        /// The name of the event.
        /// </summary>
        public string EventName { get; }
        public EventHandler(string eventName)
        {
            EventName = eventName;
        }

    }
}