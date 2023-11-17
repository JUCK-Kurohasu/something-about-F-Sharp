open System
open System.Text
open System.Net
open System.Net.Security
open System.Net.Http
open System.Security.Cryptography
open System.Text.Encodings

open System.Text.Json
open System.IO

ope
#r "nuget: FSharp.Data "
//dotnet add package FSharp.Data --version 6.3.0



Http.RequestString("https://chainflyer.bitflyer.com/v1/block/latest")

async{
    let! html=Http.AsyncRequestString("https://chainflyer.bitflyer.com/v1/block/latest")
    printfn "%d" html.Length
}|>Async.Start



































