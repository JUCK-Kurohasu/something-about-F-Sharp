open System
open System.Collections
open System.Text
open System.Net.Security
open System.Security.Cryptography
open System.Text.Encodings

//1byte=8bit(0-255)
//XByte->256bit(32byte(32文字))




type blockhead={
    index:int16
    timestamp:DateTime
    nance:bigint
    beforehash:bigint
    marklroot:bigint
}
type block={
    head:blockhead
    blockhash:bigint
    transaction:string[]
}



//基本モジュール
let rotR(target,shift)=
    (target>>>shift)+(target<<<(32-shift))


let shiR(target,shift)=
    (target>>>shift)-(0xffffffff<<<(32-shift))
    

let shiL(target,shift)=
    target<<<shift    

//パディングモジュール(Phase1)

let s0(w_red:int32):int32=
    //(rotR(w_red,7) ^^^ rotR(w_red,18)) ^^^ shiR(w_red,3)
    let a1=rotR(w_red,7)
    let a2=rotR(w_red,18)
    let a3=shiR(w_red,3)
    let ret=a1^^^a2^^^a3
    ret


let s1(w_green)=
    rotR(w_green,17) ^^^ rotR(w_green,19) ^^^ shiR(w_green,10)

let round1(w1,w2,s0arg,s1arg)=


    let S0=s0 s0arg
    let S1=s1 s1arg
    let ret=S0+S1+w1+w2
    ret
    //(fun a b  c d ->a+b+s0(c)+s1(d)) w1 w2 s0arg s1arg


//計算モジュール(Phase2~)
let temp1(h,e,K,f,g,W)=
    h+
    ( (rotR(e,6) ^^^ rotR(e,11) ^^^ rotR(e,25))+
    K+
    W+
    ((e &&& f) ^^^ ((~~~ e)&&& g))
    )


let temp2(a,b,c)=
    (rotR(a,2) ^^^ rotR(a,13) ^^^ rotR(a,22))+
    ((a&&&b) ^^^ (a&&&c) ^^^ (b&&&c))


//小さい方の素数64この立方根
let K=[0x428a2f98,0x71374491, 0xb5c0fbcf, 0xe9b5dba5, 0x3956c25b, 0x59f111f1, 0x923f82a4, 0xab1c5ed5, 0xd807aa98,
    0x12835b01, 0x243185be, 0x550c7dc3, 0x72be5d74, 0x80deb1fe, 0x9bdc06a7, 0xc19bf174, 0xe49b69c1, 0xefbe4786,
    0x0fc19dc6, 0x240ca1cc, 0x2de92c6f, 0x4a7484aa, 0x5cb0a9dc, 0x76f988da, 0x983e5152, 0xa831c66d, 0xb00327c8,
    0xbf597fc7, 0xc6e00bf3, 0xd5a79147, 0x06ca6351, 0x14292967, 0x27b70a85, 0x2e1b2138, 0x4d2c6dfc, 0x53380d13,
    0x650a7354, 0x766a0abb, 0x81c2c92e, 0x92722c85, 0xa2bfe8a1, 0xa81a664b, 0xc24b8b70, 0xc76c51a3, 0xd192e819,
    0xd6990624, 0xf40e3585, 0x106aa070, 0x19a4c116, 0x1e376c08, 0x2748774c, 0x34b0bcb5, 0x391c0cb3, 0x4ed8aa4a,
    0x5b9cca4f, 0x682e6ff3, 0x748f82ee, 0x78a5636f, 0x84c87814, 0x8cc70208, 0x90befffa, 0xa4506ceb, 0xbef9a3f7,
    0xc67178f2]



//素数8個の平方根
let hashbox=
    [0x6a09e667, 0xbb67ae85, 0x3c6ef372, 0xa54ff53a, 0x510e527f, 0x9b05688c, 0x1f83d9ab, 0x5be0cd19]






let atai:int32=rotR(6,2)
printfn "\t%32B" 6
printfn "\t%32B" atai

//開始

printfn "PUT"

let mutable str=Console.ReadLine()|>Encoding.UTF8.GetBytes
let  mutable strLength=str.Length
let mutable hai:int32[]=Array.zeroCreate 64
//エンコード用配列
let mutable strnum=str.Length*8|>int64
let str_counter=strLength/4


str<-Array.insertAt strLength (byte 10000000) str
//最後尾に0x80を追加
//Array.insertAt 挿入する場所 値 配列

for i in str.Length..63 do
    str<-Array.insertAt i (byte 00000000) str


for i in 0..7 do
   str[63-i]<-(strnum>>>i*8)|>byte


printfn "used w=0 to %d" str_counter
printfn "BYTElength:%d" str.Length


for i=0 to 15 do
    printfn "%8B %8B %8B %8B" str[i*4] str[i*4+1] str[i*4+2] str[i*4+3]


for i=0 to 15 do
    hai[i]<-(int32(str[i*4])<<<24)+(int32(str[i*4+1])<<<16)+(int32(str[i*4+2])<<<8)+(int32(str[i*4+3])<<<0)

    printfn "%32B" hai[i]


//エンコード完了。計算開始

for i=16 to 63 do
    //hai[i]<-round1(hai.[i-16],hai.[i-7],hai.[i-15],hai.[i-2])
    hai.[i]<-round1(
        (Array.get hai (i-16) ),
        (Array.get hai (i-7)),
        (Array.get hai (i-15)),
        (Array.get hai (i-2))
    )

printfn "calculated"
for i=0 to 63 do
    printfn "%d:%32B" i hai[i]


//let x=rotR(0b11011111000001111100000111011011,18)
//printfn "\n\n%32B" x




//printfn "%32B" x


let a1=rotR(0b11111101011110011001111101100000,7)
let a2=rotR(0b11111101011110011001111101100000,18)
let a3=shiR(0b11111101011110011001111101100000,3)
let aw=(a1^^^a2)//^^^a3
printfn "\n%32B\n%32B\n%32B" a1 a2 a3 
printfn "-----------------------------------------"
printfn "%32B" aw


//shiR,rotRに原因？s


