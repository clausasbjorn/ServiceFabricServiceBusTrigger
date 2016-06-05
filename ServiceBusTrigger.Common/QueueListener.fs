namespace ServiceBusTrigger.Common

open System
open System.Threading
open Microsoft.ServiceBus
open Microsoft.ServiceBus.Messaging

type QueueListener<'TMessage>(queue, callback) =

    let receiver = Queueing.receive<'TMessage> queue callback

    member this.Stop() =
        receiver.Close()