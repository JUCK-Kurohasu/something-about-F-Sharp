open System

type act()=
    member this.sigmoid(num:float)=
        let e=Math.E 
        let ret=1.0/(1.0+(e**(-num)))
        ret

    member this.relu(num:float)=
        let mutable ret=num
        ret<-System.Math.Tanh(num)
        ret

    member this.tanh(num:float)=
        let ret=Math.Tanh num
        ret





let intchecker()=
    let mutable num=0
    while num<1 do
        try 
            num<-Console.ReadLine()|>int
        with
            | :? System.FormatException->printfn "error! \n\n"
        if num<1 then
            printfn "please put again...m(__)m\n\n"
    num



    

let randcreate3D(hairetu:float[,,]):float[,,]=
    let random=new System.Random()

    let mutable i=0
    let mutable j=0
    let mutable k=0

    let height=Array3D.length1 hairetu
    let width=Array3D.length2 hairetu
    let depth=Array3D.length3 hairetu

    let return_array=Array3D.zeroCreate height width depth

    for i=0 to (height-1) do
        for j=0 to (width-1) do
            for k=0 to (depth-1) do
            return_array[i,j,k]<-(random.NextDouble()*2.0)-1.0

    return_array




let randcreate2D(hairetu:float[,]):float[,]=
    let random=new System.Random()

    let mutable i=0
    let mutable j=0

    let height=Array2D.length1 hairetu
    let width=Array2D.length2 hairetu
    let return_array=Array2D.zeroCreate height width
    for i=0 to (height-1) do 
        for j=0 to (width-1) do
            return_array[i,j]<-(random.NextDouble()*2.0)-1.0
    
    return_array

let randcreate(hairetu:float[]):float[]=
    let random=new System.Random()
    
    let mutable i=0
    let length=Array.length hairetu
    let return_array=Array.zeroCreate length
    for i=0 to (length-1) do
        return_array[i]<-(random.NextDouble()*2.0)-1.0
    
    return_array



    




printfn @"

__  __ _      _____    _             ______    _____ _                      
|  \/  | |    |  __ \  | |           |  ____|  / ____| |                     
| \  / | |    | |__) | | |__  _   _  | |__    | (___ | |__   __ _ _ __ _ __  
| |\/| | |    |  ___/  | '_ \| | | | |  __|    \___ \| '_ \ / _` | '__| '_ \ 
| |  | | |____| |      | |_) | |_| | | |       ____) | | | | (_| | |  | |_) |
|_|  |_|______|_|      |_.__/ \__, | |_|      |_____/|_| |_|\__,_|_|  | .__/ 
                               __/ |                                  | |    
                              |___/                                   |_|    "
let mutable NN:int=0
//NN<-Console.ReadLine()|>int 
while NN<2  do 
    printfn "how many layers?"
    try
        NN<-Console.ReadLine()|>int 
    with
        | :? System.FormatException-> printfn "error!!\nplease put integer...\n\n"
    if NN<2 then
        printfn "please put integer(2~)\n\n"
    else
        printfn "create %d's layers perceptron...\n\n\n" NN



let NN_IOdim:int[,]=Array2D.zeroCreate 2 NN
let mutable count=0

printfn "'dim' means dimmention\n"

for count=0 to (NN-1)do
    if count=(NN-1) then
        printfn "input dim of the layer%d\n(this value will be the dim of output )" (count+1) 
    else
        printfn "input dim of the layer%d\n" (count+1) 
    NN_IOdim[0,count]<-intchecker()
    printfn "\n"

for count=(NN-1) downto  1 do
    if count=(NN-1) then
        NN_IOdim[1,count]<-NN_IOdim[0,count]
        NN_IOdim[1,count-1]<-NN_IOdim[0,count]
    
    else 
        NN_IOdim[1,count-1]<-NN_IOdim[0,count]



let mutable kari:int[]=Array.zeroCreate NN
let mutable IOB_max:int[]=Array.zeroCreate 3    //入力、出力、層数
let NN_seq=seq{0..(NN-1)}

for count in NN_seq do
    kari[count]<-NN_IOdim[0,count]
//printfn "%A" kari
IOB_max[0]<-Array.max kari



for count in NN_seq do
    kari[count]<-NN_IOdim[1,count]
//printfn "%A \n\n\n" kari

IOB_max[1]<-Array.max kari
IOB_max[2]<-NN
//printfn "%A" IOB_max
//IOBmax=[入力の最大値,出力の最大値,層数]
//---------------------------------------------------------------------------------------------



let mutable bias:float[]=Array.zeroCreate NN
bias<-randcreate bias

let mutable weight:float[,,]=Array3D.zeroCreate IOB_max[0] IOB_max[1] IOB_max[2]
weight<-randcreate3D weight

let mutable input:float[,]=Array2D.zeroCreate IOB_max[2] IOB_max[0]     //層数*入力の最大値

let in_seq={0..(NN_IOdim[0,0]-1)}
for count in in_seq do
        printfn "%d's layers input dim" (count+1)
        input[0,count]<-Console.ReadLine()|>float
printfn "\n\n\n"

let mutable layer_count=0
let mutable node_count=0
let mutable calcu_count=0

let mutable layer_seq={0..(NN-1)}
let mutable node_seq={0..(NN-1)}
let mutable calcu_seq={0..(NN-1)}

let actfun=new act()


for layer_count in layer_seq do


    node_seq<-{0..(NN_IOdim[0,layer_count]-1)}
    for node_count in node_seq do       //layers_count層目のノード数

      if layer_count<>0 then
            input[layer_count,node_count]<-bias[layer_count]
            calcu_seq<-{0..(NN_IOdim[0,layer_count]-1)}

            for calcu_count in calcu_seq do
                input[layer_count,node_count]<-input[layer_count,node_count]+(weight[calcu_count,node_count,layer_count]*input[layer_count,node_count])
                input[layer_count,node_count]<-actfun.sigmoid input[layer_count,node_count]




//printfn "%d" NN_IOdim[1,(NN-1)]
let out_seq={0..(NN_IOdim[1,(NN-1)]-1)}
printfn "calculated\n"
for count in out_seq do
        printfn "%f" input[(NN-1),count]




                




        






        
