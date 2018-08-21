// Learn more about F# at http://fsharp.org

open System

module tuples =
    let go =
        let t1 = 12, 5, 7
        let v1, v2, _ = t1
        let t2 = "hi", true
        printfn "%A" (fst t2)
        printfn "%A" (snd t2)

        let third t =
            let _,_, r = t
            r

        printfn "%A" (third t1)

        let third' (_,_,r) = r
        printfn "%A" (third' t1)

module Options =
    let go =
        let o1 = Some(5)
        let o2 = None

        if o1 = o2 then
            printfn "Values are equal"
        
        let checkOption o =
            match o with 
            | Some(x) -> printfn "Option contains value %A" x
            | None -> printfn "Option doesn't have a value"
        
        checkOption o1
        checkOption o2

module Lists =
    let go =
        let empty = []
        let intList = [12;1;15;27]
        printfn "%A" intList

        let addItem xs x = x :: xs
        let newIntList = addItem intList 42
        printfn "%A" newIntList
        
        printfn "%A" (["hi";"there"] @ ["how"; "are"; "you"])

        printfn "%A" newIntList.Head
        printfn "%A" newIntList.Tail
        printfn "%A" newIntList.Tail.Tail.Head

        for i in newIntList do
            printfn "%A" i
        
        let rec listLength (l : 'a list) =
            if l.IsEmpty then 0
                else 1 + (listLength l.Tail)
        
        printfn "%A" (listLength newIntList)

        let rec listLength' (l : 'a list) =
            match l with
            | [] -> 0
            | _ :: xs -> 1 + (listLength' xs)
        
        printfn "%A" (listLength' newIntList)
        
        let rec listLength'' = function
            | [] -> 0
            | _ :: xs -> 1 + (listLength'' xs)
        
        printfn "%A" (listLength'' newIntList)

        let rec takeFromList n l =
            match n, l with
            | 0, _ -> []
            | _, [] -> []
            | _, (x :: xs) -> x :: (takeFromList (n-1) xs)

        printfn "%A" (takeFromList 2 newIntList)
    
module DiscriminatedUnions = 
    type MyEnum =
        | First = 0
        | Second = 1
        
    type Product =
        | OwnProduct of string
        | SupplierReference of int
    
    type Count = int

    type StockBooking =
        | Incoming of Product * Count
        | Outgoing of Product * Count
    
    type System.Int32 with
        member x.IsZero = x = 0

    type StockBooking with
        member x.IsIncomingBooking() =
            match x with
            | Incoming(_,_) -> true
            | _ -> false

    type 'a List = E | L of 'a * 'a List

    let main =
        let p1 = OwnProduct("bread")
        let p2 = SupplierReference(53)

        let bookings =
            [
                Incoming(OwnProduct("Rubber Chicken"), 50);
                Incoming(SupplierReference(112), 18);
                Outgoing(OwnProduct("Pulley"), 6);
                Outgoing(SupplierReference(37), 40)
            ]
        
        let i = 5
        printfn "%A" i.IsZero

        let booking = Incoming(SupplierReference(63), 20)
        printfn "%A" (booking.IsIncomingBooking())

        let ints = L(10, L(12, L(15, E)))
        printfn "%A" ints

        let rec listSum = function
            | E -> 0
            | L(x, xs) -> x + (listSum xs)
        printfn "%A" (listSum ints)
        
        ()

module RecordTypes =
    type Rectangle = 
        { Width: float; Height: float}
    
    type Circle = 
        { mutable Radius : float}

        member x.RadiusSquare with get() = x.Radius * x.Radius
        member x.CalcArea() = System.Math.PI * x.RadiusSquare

    type Ellipse =
        {RadiusX: float; RadiusY:float}
        member x.GrowX dx = {x with RadiusX = x.RadiusX + dx}
        member x.GrowY dy = {x with RadiusY = x.RadiusY + dy}

    let main =
        let rec1 : Rectangle = {Width =5.3; Height = 3.4}

        let c1 = {Radius = 3.3}
        c1.Radius <- 5.4
        
        let zeroCircle = function
            | {Radius = 0.0 } -> true
            | _ -> false

        printfn "%A" (zeroCircle c1)

        let isSquare = function
            | { Width = width; Height = height} -> width = height

        printfn "%A" (isSquare {Width = 5.0; Height = 5.0})

        ()
    


[<EntryPoint>]
let main argv =
    
    0 // return an integer exit code
