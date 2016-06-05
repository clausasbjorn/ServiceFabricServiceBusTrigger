namespace ServiceBusTrigger

open ServiceBusTrigger.Common.Messages
open Microsoft.ServiceFabric

module ImportantMessageBehavior =

    let received (message : ImportantMessage) =
        // Handle that important message
        ()

type ImportantMessageService(context) = 
    inherit QueueService<ImportantMessage>(context, "test-queue", ImportantMessageBehavior.received)