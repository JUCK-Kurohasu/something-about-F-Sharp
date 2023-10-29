open System
open System.Threading
open System.Text
open System.Net.Security
open System.Net.Http
open System.Security.Cryptography
open System.Text.Encodings

open System.Text.Json
open System.IO
type user={
   name:string
   age:int
}






let message= @"num1=01,num2=02,num3=03"
let str=message.Split(",")
for i=0 to 2 do 
   printfn "%s" str[i] 



let testuser:user={
   name="K"
   age=16
}

let json=JsonSerializer.Serialize(testuser)
File.WriteAllText("user.json",json)


let testtask=
   async{
     task.Delay(2000)
     printfn "hello"
   }


testtask()














