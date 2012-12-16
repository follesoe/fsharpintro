// Defining simple units of measure
[<Measure>] type m
[<Measure>] type s
[<Measure>] type cm
[<Measure>] type inch
[<Measure>] type foot
[<Measure>] type kg

let x = 1<cm>       // int
let y = 1.0<cm>     // float
let z = 1.0m<cm>    // decimal

let acceleration = 2.0<m/s^2>

// Derived unit of measure
[<Measure>] type N = kg m/s^2
let forc = 5.0<kg m/s^2>
let forc2 = 5.0<N>

// Type checkign and type inference
let distance = 3.0<cm>
let distance2 = distance * 2.0

let addThreeCm dist =
    dist + 3.0<cm>

// addThreeCm(10<inch>) // does not compile

let dist = 100.0<m>
let time = 60.0<s>
let speed = dist/time
let acc = speed/time
let mass = 5.0<kg>
let force = mass * speed/time

// Conversion
let inchesPerFoot = 12.0<inch/foot>
let distanceInFeet = 3.0<foot>
let distanceInInches = distanceInFeet * inchesPerFoot

// Temp.
[<Measure>] type degC
[<Measure>] type degF

let convertDegCToF c =
    c * 1.8<degF/degC> + 32.0<degF>

let f = convertDegCToF 0.0<degC>

// Currency
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

// Examples

let gravity = 9.81<m/s^2>
let heightOfChimney = 15.0<m>

let speedOfImpact = sqrt(2.0 * gravity * heightOfChimney)

printfn "Santa will hit the floor at %A m/s" speedOfImpact