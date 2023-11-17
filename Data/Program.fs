open System
open System.Text
open System.Net
open System.Net.Security
open System.Net.Http
open System.Security.Cryptography
open System.Text.Encodings

open System.Text.Json
open System.IO

open FSharp.Data



let mutable str=Http.RequestString("https://chainflyer.bitflyer.com/v1/block/000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f")
printfn "%s" str

str<-Http.RequestString("https://chainflyer.bitflyer.com/v1/block/00000000839a8e6886ab5951d76f411475428afc90947ee320161bbf18eb6048")
printfn "%s" str

str<-Http.RequestString("https://chainflyer.bitflyer.com/v1/block/000000006a625f06636b8bb6ac7b960a8d03705d1ace08b1a19da3fdcc99ddbd")
printfn "%s" str

let ret=JsonValue.AsyncLoad(str)
let J=JsonSerializer.Serialize(ret)
File.WriteAllText("coin.json",json)








