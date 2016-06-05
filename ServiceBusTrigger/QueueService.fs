namespace ServiceBusTrigger

open System.Fabric
open Microsoft.ServiceFabric.Services.Communication.Runtime;
open Microsoft.ServiceFabric.Services.Runtime;
open ServiceBusTrigger.Common

type QueueService<'TMessage>(serviceContext, queueName, callback) = 
    inherit StatelessService(serviceContext)

    let mutable queue : Option<QueueListener<'TMessage>> = None

    let stop () =
        match queue with
        | None -> ()
        | Some q -> q.Stop()

    override this.OnAbort () =
        stop()
        base.OnAbort()

    override this.OnCloseAsync(cancellationToken) =
        stop()
        base.OnCloseAsync(cancellationToken)

    override this.OnOpenAsync(cancellationToken) =
        queue <- Some (new QueueListener<'TMessage>(queueName, callback))
        base.OnOpenAsync(cancellationToken)
