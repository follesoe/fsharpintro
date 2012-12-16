(*  Define Units of Measure as 
    types annotated with the Measure-annotation *)

[<Measure>] type kg
[<Measure>] type s
[<Measure>] type m
[<Measure>] type cm
[<Measure>] type km
[<Measure>] type h            
[<Measure>] type ml = cm^3

// Units are attached to integers or floating point numbers.
let gravityOnEarth = 9.81<m/s^2>
let heightOfJump = 3.5<m>

// F# works out the new units based operations applied to the values.
let speedOfImpact = sqrt (2.0 * gravityOnEarth * heightOfJump)

(*  Can convert between units by defining conversion
    constants and helper functions applying the conversions. *)
let kmToM = 1000.0<m/km>
let hrToSec = 3600.0<s/h>

let msToKmph(speed : float<m/s>) =
    speed / kmToM * hrToSec

let speedOfImpactKmh = msToKmph(speedOfImpact)


(*  Using Newton's Second Law of Motion  
    to calculate the force I apply on the earth *)
let myMass = 82.0<kg>
let forceOnGround = myMass * gravityOnEarth

// Creating a derived unit of measrue: Newton
[<Measure>] type N = kg m/s^2

let forceOnGroundInNewton:float<N> = myMass * gravityOnEarth

(*  Units can be defined recursively and 
    have attached functions *)
[<Measure>]
type far = 
    static member ConvertToCel(x : float<far>) =
        (5.0<cel> / 9.0<far>) * (x - 32.0<far>)
and [<Measure>] cel = 
    static member ConvertToFar(x : float<cel>) =
        (9.0<far> / 5.0<cel> * x) + 32.0<far>

far.ConvertToCel(100.0<far>)
cel.ConvertToFar(50.0<cel>)


(*  Units of Measures accepted by functions can be
    defined using generics *)     
let squareMeter (x : float<m>) = x * x
let squareCentimeter (x : float<cm>) = x * x
let genericSequare (x : float<_>) = x * x

squareMeter 10.0<m>
genericSequare 10.0<m>

(*  Units can be used in type declarations *)    
type CurrencyRate<[<Measure>]'u, [<Measure>]'v> =
    { Rate: float<'u/'v>; Date: System.DateTime }

[<Measure>] type EUR
[<Measure>] type USD
[<Measure>] type GBP

let mar1 = new System.DateTime(2012, 3, 1)
let eurToUsdOnMar1 = {Rate=1.2<USD/EUR>; Date=mar1}
let eurToGbpOnMar1 = {Rate=0.8<GBP/EUR>; Date=mar1}

let tenEur = 10.0<EUR>
let tenEurInUsd = eurToUsdOnMar1.Rate * tenEur

// SI units defined in the F# standard library
//Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
//Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols