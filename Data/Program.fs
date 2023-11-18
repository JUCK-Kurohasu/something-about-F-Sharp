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
open FSharp.Data.JsonExtensions

type block={
    block_hash:string       //ブロックのハッシュ
    height:uint64       //ブロックの高さ(何個目のブロックか)
    is_main:bool        //メインチェーンだとtrue
    version:uint64      //ブロックのバージョン
    prev_block:string   //前ブロックのハッシュ
    merkle_root:string  //マークルルートのハッシュ
    timestamp:System.DateTime   //ブロックが作成された時の時間
    bits:uint64     //マイニング難易度
    nonce:uint64    //ナンス(PoW)　ここを探す！！
    txnum:uint64    //トランザクションの数
    total_fees:uint64   //手数料(satoshi単位)
    tx_hashes:string[]  //ブロックに含まれるtxID配列

}

type tx={
    tx_hashes:string
    block_height:uint64
    confirmed:int64

}

let mutable str=Http.RequestString("https://chainflyer.bitflyer.com/v1/block/000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f")
printfn "%s" str

//str<-Http.RequestString("https://chainflyer.bitflyer.com/v1/block/00000000839a8e6886ab5951d76f411475428afc90947ee320161bbf18eb6048")
//printfn "%s" str

//str<-Http.RequestString("https://chainflyer.bitflyer.com/v1/block/000000006a625f06636b8bb6ac7b960a8d03705d1ace08b1a19da3fdcc99ddbd")
//printfn "%s" str

let data=JsonValue.Load("https://chainflyer.bitflyer.com/v1/block/000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f")
//printfn "%A" ret
//let J=JsonValue.WriteTo()

let p=data?bits
printfn "bit:%s" (p.AsString())

//let atai=p?height
//printfn "%A" atai


let export:block={
    block_hash=(data?block_hash.AsString())
    height=(data?height.AsInteger64())|>uint64
    is_main=(data?is_main.AsBoolean())
    version=(data?version.AsInteger64())|>uint64
    prev_block=(data?prev_block.AsString())
    merkle_root=(data?merkle_root.AsString())
    timestamp=(data?timestamp.AsDateTime())
    bits=(data?bits.AsInteger64())|>uint64
    nonce=(data?nonce.AsInteger64())|>uint64
    txnum=(data?)
}

printfn "%A" export
