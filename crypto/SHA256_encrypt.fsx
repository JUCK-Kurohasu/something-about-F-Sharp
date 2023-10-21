open System
open System.Collections
open System.Text
open System.Net.Security
open System.Security.Cryptography
open System.Text.Encodings



//基本モジュール
let rotR(target,shift)=
    (target>>>shift)+(target<<<(32-shift))
    


let shiR(target,shift)=
    (uint32 target)>>>shift|>int32

let shiL(target,shift)=
    target<<<shift    

//パディングモジュール(Phase1)

let s0(w_red:uint32):uint32=
    (rotR(w_red,7) ^^^ rotR(w_red,18)) ^^^ (shiR(w_red,3)|>uint32)



let s1(w_green)=
    rotR(w_green,17)^^^rotR(w_green,19)^^^(shiR(w_green,10)|>uint32) 


let round1(w1,w2,s0arg,s1arg)=
    (fun a b  c d ->a+b+s0(c)+s1(d)) w1 w2 s0arg s1arg
    




//計算モジュール(Phase2~)

let S0(w)=
    rotR(w,2)^^^rotR(w,13)^^^rotR(w,22)


let S1(w)=
    rotR(w,6)^^^rotR(w,11)^^^rotR(w,25)


let M(a,b,c)=
    (a &&& b)^^^(a &&& c) ^^^(b &&& c)  //OK

let C(e,f,g)=
    (e&&&f)^^^((~~~e)&&&g)  //OK


let temp1(h,e,f,g,k,w)= //OK?
    h+S1(e)+C(e,f,g)+k+w



let temp2(a,b,c)=   //OK
    S0(a)+M(a,b,c)
    


//小さい方の素数64この立方根
let K=[|
    0x428a2f98ul; 0x71374491ul; 0xb5c0fbcful; 0xe9b5dba5ul; 0x3956c25bul; 0x59f111f1ul; 0x923f82a4ul; 0xab1c5ed5ul; 0xd807aa98ul;
    0x12835b01ul; 0x243185beul; 0x550c7dc3ul; 0x72be5d74ul; 0x80deb1feul; 0x9bdc06a7ul; 0xc19bf174ul; 0xe49b69c1ul; 0xefbe4786ul;
    0x0fc19dc6ul; 0x240ca1ccul; 0x2de92c6ful; 0x4a7484aaul; 0x5cb0a9dcul; 0x76f988daul; 0x983e5152ul; 0xa831c66dul; 0xb00327c8ul;
    0xbf597fc7ul; 0xc6e00bf3ul; 0xd5a79147ul; 0x06ca6351ul; 0x14292967ul; 0x27b70a85ul; 0x2e1b2138ul; 0x4d2c6dfcul; 0x53380d13ul;
    0x650a7354ul; 0x766a0abbul; 0x81c2c92eul; 0x92722c85ul; 0xa2bfe8a1ul; 0xa81a664bul; 0xc24b8b70ul; 0xc76c51a3ul; 0xd192e819ul;
    0xd6990624ul; 0xf40e3585ul; 0x106aa070ul; 0x19a4c116ul; 0x1e376c08ul; 0x2748774cul; 0x34b0bcb5ul; 0x391c0cb3ul; 0x4ed8aa4aul;
    0x5b9cca4ful; 0x682e6ff3ul; 0x748f82eeul; 0x78a5636ful; 0x84c87814ul; 0x8cc70208ul; 0x90befffaul; 0xa4506cebul; 0xbef9a3f7ul;
    0xc67178f2ul|]



//素数8個の平方根
let hashbox=
    [|0x6a09e667ul; 0xbb67ae85ul; 0x3c6ef372ul; 0xa54ff53aul; 0x510e527ful; 0x9b05688cul; 0x1f83d9abul; 0x5be0cd19ul|]







//開始

printfn "PUT"
let beforestr=Console.ReadLine()
let mutable str=beforestr|>Encoding.UTF8.GetBytes
let  mutable strLength=str.Length
let mutable hai:uint32[]=Array.zeroCreate 64
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


//printfn "used w=0 to %d" str_counter
//printfn "BYTElength:%d" str.Length
//for i=0 to 15 do
    //printfn "%8B %8B %8B %8B" str[i*4] str[i*4+1] str[i*4+2] str[i*4+3]

for i=0 to 15 do
    hai[i]<-(uint32(str[i*4])<<<24)+(uint32(str[i*4+1])<<<16)+(uint32(str[i*4+2])<<<8)+(uint32(str[i*4+3])<<<0)

    //printfn "%32B" hai[i]


//エンコード完了。計算開始

for i=16 to 63 do
    hai[i]<-round1(hai.[i-16],hai.[i-7],hai.[i-15],hai.[i-2])


//printfn "calculated"
//for i=0 to 63 do
//    printfn "%d:\t%32B" i hai[i]

printfn "ENCODED.\nGO PHASE2...\n\n\n"



//出力配列
let mutable hash:uint32[]=Array.zeroCreate 8


for i=0 to 7 do
    hash[i]<-(hashbox[i]|>uint32)
    //printfn "sqrt %d=%32B" sosuu[i] hash[i] 



for i=0 to 63 do
    let t1=temp1(hash[7],hash[4],hash[5],hash[6],K[i],hai[i])
    let t2=temp2(hash[0],hash[1],hash[2])
    //printfn "%d's temp1=%32B" i t1
    //printfn "%d's temp2=%32B\n\n" i t2

    hash[7]<-hash[6]
    hash[6]<-hash[5]
    hash[5]<-hash[4]
    hash[4]<-(hash[3]+t1)
    hash[3]<-hash[2]
    hash[2]<-hash[1]
    hash[1]<-hash[0]
    hash[0]<-(t1+t2)


let mutable finalhash: string=null
for i=0 to 7 do

   
    hash[i]<-(hash[i]+hashbox[i])
    printfn "%d:%32B" i hash[i]
    finalhash<-finalhash+hash[i].ToString("x")

printfn "calculated.\n"
printfn "( %s )'s SHA-256=" beforestr

printfn "%s" finalhash









