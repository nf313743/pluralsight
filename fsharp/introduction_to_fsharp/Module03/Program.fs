type CarType =
    | Tricar = 0
    | StandardFourWheeler = 1
    | HeavyLoadCarrier = 2
    | ReallyLargeTruck = 3
    | CrazyHugeMythicalMonster = 4
    | WeirdContraption = 5

type Car(colour:string, wheelCount:int) =
    do
        if wheelCount < 3 then failwith "We'll assume that cars must have 3 wheels at least."
        if wheelCount > 99 then failwith "That's ridiculous."

    let carType =
        match wheelCount with
        | 3 -> CarType.Tricar
        | 4 -> CarType.StandardFourWheeler
        | 6 -> CarType.HeavyLoadCarrier
        | x when x % 2 = 1 -> CarType.WeirdContraption
        | _ -> CarType.CrazyHugeMythicalMonster

    let mutable passengerCount = 0
    new() = Car("red", 4)
    member x.Move() = printfn "The %s car (%A) is moving" colour carType
    member x.CarType = carType
    
    abstract PassengerCount: int with get, set
    default x.PassengerCount with get() = passengerCount and set v = passengerCount <- v
    //member x.PassengerCount with get() = passengerCount and set v = passengerCount <- v

type Red18Wheeler() =
    inherit Car("red", 18)

    override x.PassengerCount
        with set v = 
            if v > 2 then failwith "only two passengers allowed."
            else base.PassengerCount <- v
let car = Car()
car.Move()

let greenCar = Car("green", 4)
greenCar.Move()
printfn "greenCar has type %A" greenCar.CarType
greenCar.PassengerCount <- 5
printfn "PassengerCount: %A" greenCar.PassengerCount

let truck = Red18Wheeler()
truck.PassengerCount <- 1

let truckObject = truck :> obj
let truckCar = truck :> Car // Upcast.
let truckObjectBackToCar = truckObject :?> Car // Downcast.