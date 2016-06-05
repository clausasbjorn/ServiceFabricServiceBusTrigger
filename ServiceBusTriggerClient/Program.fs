open System
open ServiceBusTrigger.Common
open ServiceBusTrigger.Common.Messages

[<EntryPoint>]
let main argv = 
    
    Queueing.send "test-queue" { Message = "Very important stuff!" }

    0 // return an integer exit code
