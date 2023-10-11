open System
open System.Text
open System.Text.Encodings
open System.Security.Cryptography




printfn "入力"
let str=Console.ReadLine()|>Encoding.UTF8.GetBytes
let ch=str.Length

let hai:int32[]=Array.zeroCreate 64

let karibox:int8[]=Array.zeroCreate 4

for i=0 to 13 do
    try hai[(i)] <-2 with
        | :? System.IndexOutOfRangeException->
        printfn "error!"
        hai[(i)]<-int32 0

    try hai[(i+1)] <-2 with
        | :? System.IndexOutOfRangeException->
        printfn "error!"
        hai[(i+1)]<-int32 0

    try hai[(i+2)] <-2 with
        | :? System.IndexOutOfRangeException->
        printfn "error!"
        hai[(i+3)]<-int32 0

    try hai[(i+3)] <-2 with
        | :? System.IndexOutOfRangeException->
        printfn "error!"
        hai[(i+3)]<-int32 0




printfn "%B" hai[53]






