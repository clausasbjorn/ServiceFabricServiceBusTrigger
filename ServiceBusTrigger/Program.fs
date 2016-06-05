open System
open System.Threading
open System.Threading.Tasks
open Microsoft.ServiceFabric.Services.Runtime
open ServiceBusTrigger

[<EntryPoint>]
let main argv = 

    try
        ServiceRuntime.RegisterServiceAsync("ServiceBusTriggerType", fun context -> new ImportantMessageService(context) :> StatelessService).GetAwaiter().GetResult()
        Thread.Sleep(Timeout.Infinite)
    with
    | :? Exception as e -> ()

    0
