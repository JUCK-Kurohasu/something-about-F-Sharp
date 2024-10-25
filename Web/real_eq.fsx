#r "nuget: FSharp.Data, 6.4.0"
open System
open System.Net
open System.Text.Encodings
open System.Net.WebSockets
open System.Threading
open FSharp.Data
open FSharp.Data.JsonExtensions

[<Literal>]
let wslink="wss://api-realtime-sandbox.p2pquake.net/v2/ws"
let url=new Uri(wslink)
let client=new ClientWebSocket()
try
    client.ConnectAsync(url,cancellationToken=CancellationToken.None)
        |>Async.AwaitTask|>Async.RunSynchronously
with
    :? System.AggregateException ->
        printfn "ネットワークの接続に失敗しました"
        exit -1
printfn "接続成功！"
let buf:array<byte>=Array.zeroCreate 4096
task{
    while true do
        let! res=client.ReceiveAsync(new ArraySegment<byte>(buf),CancellationToken.None)
        if client.HttpStatusCode=HttpStatusCode.NotFound then 
            printfn "404 により中止します"
            return ()
        else 
            let reci=Text.Encoding.UTF8.GetString(buf,0,res.Count)
            //printfn "%s\n" reci
            let mutable inf=JsonValue.Parse(reci)
            Console.Clear()
            printfn "地震発生 %s" <|inf?earthquake?time.AsString()
            printfn "震源:%s(M %s)" (inf?earthquake?hypocenter?name.AsString()) (inf?earthquake?hypocenter?magnitude.AsString())
            printfn "最大震度 %d" <|((inf?earthquake?maxScale.AsString()|>Int32.Parse)/10)
            



            
}|>Async.AwaitTask|>Async.RunSynchronously



