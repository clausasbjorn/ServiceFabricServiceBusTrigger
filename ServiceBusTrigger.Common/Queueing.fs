namespace ServiceBusTrigger.Common

open System
open System.Threading
open Microsoft.ServiceBus
open Microsoft.ServiceBus.Messaging

module Queueing =

    let private connectionString = @"<YOUR SERVICEBUS CONNECTION STRING HERE>"

    let send queue (message : Object) =
        let namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString)
        match namespaceManager.QueueExists(queue) with
        | false -> namespaceManager.CreateQueue(queue) |> ignore
        | _ -> ()

        let client = QueueClient.CreateFromConnectionString(connectionString, queue)
        
        new BrokeredMessage(message)
        |> client.Send

    let internal receive<'TMessage> queue callback =
        let namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString)
        match namespaceManager.QueueExists(queue) with
        | false -> namespaceManager.CreateQueue(queue) |> ignore
        | _ -> ()

        let options = new OnMessageOptions();
        options.MaxConcurrentCalls <- 1
        options.AutoComplete <- true

        let client = QueueClient.CreateFromConnectionString(connectionString, queue)
        client.OnMessage((fun msg -> msg.GetBody<'TMessage>() |> callback), options)
        
        client
