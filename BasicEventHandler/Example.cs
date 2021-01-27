//   Written by NoSharp
//   Copyright Notice
/*
    © 2020 Elitelupus All rights reserved. 

    All rights in this application are owned by Elitelupus and legally protected under applicable intellectual
    property laws.

    Your possession, access, and use of this application or any part of the same does not 
    transfer to you or any third party any rights, title, or interest in or to the application 
    or intellectual property rights.

    You may not use this application or any component therein for any commercial use or reproduce,
    distribute or transmit the same in any form or by any means without the prior written consent 
    of the owner.
*/


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