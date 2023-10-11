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




let rotR(target,shift)=
    (target>>>shift)+(target<<<(32-shift))


let shiR(target,shift)=
    target>>>shift
    

let shiL(target,shift)=
    target<<<shift    


let round1(w1,w2,s0,s1)=
    w1+w2+ rotR(s0,7) ^^^ rotR(s0,18) ^^^ shiR(s0,3)+
    rotR(s1,17) ^^^ rotR(s1,14) ^^^ shiR(s1,10)


let temp1(h,e,K,f,g,W)=
    h+( rotR(e,6) ^^^ rotR(e,11) ^^^ rotR(e,25)+K+W+((e &&& f) ^^^ ((~~~ e)&&&g)) )


let temp2(a,b,c)=
    rotR(a,2) ^^^ rotR(a,13) ^^^ rotR(a,22)+(a&&&b) ^^^ (a&&&c) ^^^ (b&&&c)


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


printfn "PUT"
let str=Console.ReadLine()|>Encoding.UTF8.GetBytes

let MSB=(str.Length)*8|>int64

let n=str.Length
let a=str.Length%4
//str[n]<-(byte 0x80)
//4charごとに分割
printfn "%d " n

if n<14 then
    printfn "%A" str
    
let mutable count=0
let  word:int32[]=Array.create 64 0


for count=0 to ((n/4)-1) do
    printfn "%32B" str[count]
    printfn "%32B" str[count+1]
    printfn "%32B" str[count+2]
    printfn "%32B" str[count+3]
    word[count]<-shiL((int32 str[count]),24)+shiL((int32 str[count+1]),16)+shiL((int32 str[count+2]),8)+shiL((int32 str[count+3]),0)

    printfn "%32B" word[count]
    printfn "\n"
    

count<-(n/4)
printfn "%d" count
printfn "MSB=%64d" MSB


 







