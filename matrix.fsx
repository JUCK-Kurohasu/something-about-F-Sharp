open System


let multi(a,b)=a*b


let matrix(ar1,ar2)=
    let mutable ar1num=Array2D.length2 ar1
    let mutable ar2num=Array2D.length1 ar2
    if ar1num<>ar2num then raise (System.ArgumentException("ERROR!"))
    let kyoutu=Array2D.length2 ar1  //共通

    ar1num<-Array2D.length1 ar1
    ar2num<-Array2D.length2 ar2

    let ra=Array2D.zeroCreate ar1num ar2num
    let mutable box=0

    for m1=0 to (ar1num-1) do
        for m2=0 to (ar2num-1) do
            for i in 0..(kyoutu-1) do
                box<-box+(ar1[m1,i]*ar2[i,m2])
            
            ra[m1,m2]<-box

    //printfn "%A" ra
    ra



let hai1=Array2D.init 2 4 (fun x y->x+y)
let hai2=Array2D.init 4 2 (fun x y->x*y)
printfn "%A" hai1
printfn "%A" hai2

let num=matrix(hai1,hai2)
printfn "\n\n%A" num


















