open System
open System.Text
open System.Net.Security
open System.Security.Cryptography



type originalMath()=
    static member GCD(x1,x2)=
        let mutable a,b=x1,x2
        let mutable boxa=0
        while b<>0 && b<>1 do
            boxa<-a
            a<-b
            b<-boxa%b    
    
        match b with
            |0->a
            |1->1
            |_-> -1

    static member GCD(x1,x2)=
        let mutable (a:uint64),(b:uint64)=x1,x2
        let mutable boxa=0UL
        while b<>0UL && b<>1UL do
            boxa<-a
            a<-b
            b<-boxa%b    
    
        match b with
            |0UL->a
            |1UL->1UL
            |_-> uint64 -1

    static member GCD(x1,x2)=
        let mutable (a:decimal),(b:decimal)=x1,x2
        let mutable boxa=0m
        while b<>0m && b<>1m do
            boxa<-a
            a<-b
            b<-boxa%b    
    
        match b with
            |0m->a
            |1m->1m
            |_-> -1m
        


    static member power(x,y)=
        let mutable back=1
        for i in 1..1..y do back<-back*x
        back

    static member power(x:uint64,y:uint64)=
        let mutable back=1UL
        for i in 1UL..1UL..y do back<-back*x
        back

    static member power(x:decimal,y:decimal)=
        let mutable back=1m
        for i in 1m..1m..y do back<-back*x
        back

    static member power(x:double,y:double)=
        let mutable back=1.0
        for i in 1.0..1.0..Double.Round(y) do back<-back*x
        back



    static member extend_euclid(a:int,b)=
        if b=0 then 
            (1,0)
        else
            (1,2)


