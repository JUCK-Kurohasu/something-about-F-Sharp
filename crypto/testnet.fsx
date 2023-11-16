open System
open System.Text
open System.Net.Security
open System.Net.Http
open System.Security.Cryptography
open System.Text.Encodings

open System.Text.Json
open System.IO

#r "nuget: FSharp.Data "
//dotnet add package FSharp.Data --version 6.3.0

let url= @"https://chainflyer.bitflyer.com/v1/block/000000000019d6689c085ae165831e934ff763ae46a2a6c172b3f1b60a8ce26f"
let clientbox=new HttpClient()
async{
   let! response=clientbox.GetAsync(url)|>Async.AwaitTask
   let content=response.Content
   let! data=content.ReadAsStringAsync()|>Async.AwaitTask
   printfn "%s %s\n\n" url data 
   let mutable  stri=data.Split(",")
   

   for i=0 to ((stri.Length)-1) do
    printfn "%s" stri[i]

    //let str=data.split(",")|>Async.AwaitTask
    //printfn "%A" str
}
|>Async.RunSynchronously
printfn "end!"




//https://fsharp.github.io/fsharp-core-docs/reference/fsharp-control-fsharpasync.html を参考程度に(非同期処理のこと書いてある)
//(最新のブロックのデータを引っ張る)
//
//-------------------------------------------------------------------------------------------
//
//clientをHttpClientクラスとして使用
//urlを設定
//
//async(非同期に以下を実行){
//
//  非同期でresponseを宣言<-非同期でurlに非同期接続  |> タスク完了まで待機  
//  contentを宣言<-responseの取得内容
//  非同期でdataを宣言<-contentを非同期で文字列型にする  |>　タスク完了まで待機
//  urlとdataを出力
//
//  }|>ここまでの非同期計算を実行し、終わるまで待機
//




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







