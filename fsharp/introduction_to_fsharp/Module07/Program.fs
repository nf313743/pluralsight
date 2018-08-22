// Learn more about F# at http://fsharp.org

open System

module Exceptions =
    exception MyException of int * string
        with 
            override x.Message =
                let (MyException(i, s)) = upcast x
                sprintf "Int: %d Str:%s" i s

    let main =
        //raise (MyException(10, "Error!"))

        //failwith "Some error occurred"

        let rec fact x =
            if x < 0 then invalidArg "x" "Value must be >= 0"
            match x with
            | 0 -> 1
            | _ -> x * (fact (x - 1))
    
        let output (o: obj) =
            try
                let os = o :?> string
                printfn "Object is %s" os
            with
            | :? System.InvalidCastException as ex -> printfn "Can't cast, message was: %s" ex.Message
        
        //output 35

        let result =
            try
                Some(10 /0)
            with
            | :? System.DivideByZeroException -> None
        //printfn "%A" result

        // try
        //     raise(MyException(3, "text"))
        // with
        // | MyException(i, s) -> printfn "Hello %d %s" i s

        let getValue() =
            try
                printfn "Returning Value"
                42
            finally
                printfn "In the finally block now"
        
        getValue()

module Disposable =
    let createDisposable f =
        {new System.IDisposable with member x.Dispose() = f()}

    let outerFunction() =
        use disposable = createDisposable(fun () -> printfn "Now disposing of myself")
        printfn "In outer function"

    let outerFunction'() =
        using (createDisposable( fun () -> printfn "Now disposing of myself"))
            (fun d -> printfn "Acting on the disposable object now")
        printfn "In outer function"

    let main =
        outerFunction()
        outerFunction'()


[<EntryPoint>]
let main argv =
    printfn "Hello World from F#!"
    0 // return an integer exit code
