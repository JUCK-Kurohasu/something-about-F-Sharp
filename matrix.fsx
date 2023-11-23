open System


let multi(a,b)=a*b


let matrix(ar1,ar2)=
    let mutable ar1num=Array2D.Length2 ar1
    let mutable ar2num=Array2D.length1 ar2
    if ar1num<>ar2num then raise (System.ArgumentException("ERROR!"))

    ar1num<-Array2D.Length1 ar1
    ar2num<-Array2D.Length2 ar2

    let ra=Array2D.zeroCreate ar1num ar2num

    for m1=0 to (ar1num-1) do
        for m2=0 to (ar2num-1) do
            ra[m1,m2]<-
















