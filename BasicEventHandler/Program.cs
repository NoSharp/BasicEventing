using BasicEventHandler.Eventing;

namespace BasicEventHandler
{
    internal class Program
    {
        public static EventManager EventManagerInstance = EventManager.Instance;
        
        public static void Main(string[] args)
        {
            EventManagerInstance.RegisterEventHandlers(typeof(Example));
            EventManagerInstance.CallEvent("HelloWorld", true, null);
        }
    }
}