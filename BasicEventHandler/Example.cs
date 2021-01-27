//   Written by NoSharp


using System;

namespace BasicEventHandler
{

    public class Example
    {
        
        [Eventing.EventHandler("HelloWorld")]
        public static void HelloWorldEvent(){
            Console.WriteLine("Hello world!");
        }
    }
}
