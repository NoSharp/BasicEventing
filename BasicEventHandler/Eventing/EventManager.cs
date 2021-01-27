using System;
using System.Collections.Generic;
using System.Reflection;

namespace BasicEventHandler.Eventing
{
    /// <summary>
    /// Used to manage and trigger other events.
    /// </summary>
    public sealed class EventManager
    {

        private static readonly object InstanceLock = new object();
        private static EventManager _instance;
        private Dictionary<string, List<MethodInfo>> _events = new Dictionary<string, List<MethodInfo>>();
        
        /// <summary>
        /// Singleton, Instantiates class.
        /// </summary>
        private EventManager()
        {
        }

        /// <summary>
        /// The events currently registered.
        /// </summary>
        public Dictionary<string, List<MethodInfo>> Events
        {
            get => _events;
            private set => _events = value;
        }

        /// <summary>
        /// Return the instance to this class.
        /// </summary>
        public static EventManager Instance
        {
            get
            {
                lock (InstanceLock)
                {
                    if (_instance == null)
                    {
                        _instance = new EventManager();
                    }
                    
                    return _instance;
                }
            }
        }

        /// <summary>
        /// Used to call all functions registered with 
        /// that are associated with the event being called,
        /// Note: Some events may not return an object, and
        /// will return null.
        /// Addendum: This event will stop at the first value
        /// returned.
        /// </summary>
        /// <param name="eventName"> The event's name. </param>
        /// <param name="noResult">
        /// Should the function return the results of events
        /// If this is true, then it will not return a result
        /// and will not stop at the first function which returns
        /// a value.
        /// </param>
        /// <param name="arguments"> The arguments that we're passing to the event.</param>
        /// <returns>
        /// Any result from the event handlers called.
        /// Will be null if noResult is true.
        /// </returns>
        public object CallEvent( string eventName, bool noResult, params object[] arguments)
        {
            List<MethodInfo> callbacks = GetCallbacks(eventName);
            for (var i = 0; i < callbacks.Count; i++)
            {
                MethodInfo info = callbacks[i];
                object result = info.Invoke(null, arguments);
                if (result != null && !noResult)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Get's the callbacks for an event.
        /// </summary>
        /// <param name="eventName"> The event name </param>
        /// <returns>Null if no event with that name is found or the callbacks.</returns>
        public List<MethodInfo> GetCallbacks(string eventName) {
            if (!this.Events.ContainsKey(eventName))
            {
                return null;
            }

            return this.Events[eventName];
        }

        /// <summary>
        /// Registers an EventHandler based on it's event name
        /// and the defined callback.
        /// </summary>
        /// <param name="eventName"> The name of the event. </param>
        /// <param name="callback"> The method that should be invoked when calling the event. </param>
        public void RegisterEventHandler(string eventName, MethodInfo callback)
        {
            if (!this.Events.ContainsKey(eventName))
            {
                this.Events.Add(eventName, new List<MethodInfo>());
            }
            this.Events[eventName].Add(callback);
        }

        /// <summary>
        /// Used to register all the events in a class.
        /// It's expected that these methods are static.
        /// </summary>
        /// <param name="eventOwner">The Class/Type we're iterating over.</param>
        public void RegisterEventHandlers(Type eventOwner)
        {
            MethodInfo[] methods = eventOwner.GetMethods();
            for (var i = 0; i < methods.Length; i++)
            {
                MethodInfo method = methods[i];
                object[] attributes = method.GetCustomAttributes(typeof(EventHandler), false);
                if (attributes.Length == 0) continue;

                foreach (var attribute in attributes)
                {
                    if (!(attribute is EventHandler)) continue;

                    EventHandler eventHandler = (EventHandler)attribute;
                    this.RegisterEventHandler(eventHandler.EventName, method);
                }
            }
        }

    }
}