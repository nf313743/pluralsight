namespace Module04

open System.Collections.Generic
module Functions = 
    let add x y = x + y
    let add' = fun x y -> x + y

    // let checkThis item f = 
    //     if f item then
    //         printfn "HIT" 
    //     else
    //         printfn "MISS"
    let checkThis (item: 'a) (f: 'a -> bool): unit = 
        if f item then
            printfn "HIT" 
        else
            printfn "MISS"

module PartialApplication =
    let add x y = x + y
    let mult x y = x * y
    let add'' x = fun y -> x + y
    let foo = add'' 10
    let add10'' = add'' 10

    let add10 = add 10

    let go =
        printfn "%d" (add10'' 32)
        printfn "%d" (add10 32)

        let mult5 = mult 5
        let calcResult = mult5 (add10 17)
        let calcResult' = 17 |> add10 |> mult5
        let add10Mult5 = add10 >> mult5
        let calcResult'' = 17 |> add10Mult5
        printfn "%d" calcResult''
        ()

module FunctionalAlgorithms = 
    let isInList(list: 'a list) v =
        let lookupTable = new HashSet<'a>(list)
        printfn "Lookup table created, looking up value"
        lookupTable.Contains v
     
    let constructLookup (list: 'a list) =
        let lookupTable = new HashSet<'a>(list)
        printfn "Lookup created 2"
        fun v ->
            printfn "Performing lookup"
            lookupTable.Contains v
    
    let go =
        //bad
        printfn "%b" (isInList ["hi"; "there"; "oliver"] "there")
        printfn "%b" (isInList ["hi"; "there"; "oliver"] "anna")

        //better
        let isInListClever = constructLookup["hi"; "there"; "oliver"]
        printfn "%b" (isInListClever "there")
        printfn "%b" (isInListClever "anna")
        ()


module Recursion =
    open System
    open System.Threading
    let rec fact x = 
        if x = 1 then 1
        else x * (fact(x-1))

    let rec fnB() = fnA()
    and fnA() = fnB()

    let showValues() =
        let r = Random()
        let rec dl() =
            printfn "%d" (r.Next())
            Thread.Sleep(1000)
            dl()
        dl()
        // while true do
        //     printfn "%d" (r.Next())
        //     Thread.Sleep(1000)



    let go =
        //printfn "%d" (fact 5)
        showValues()
        ()

module Main =
    [<EntryPoint>]
    let main args = 
        //Functions.checkThis 5 (fun x -> x > 3)
        //PartialApplication.go
        //FunctionalAlgorithms.go
        0
