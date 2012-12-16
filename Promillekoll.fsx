#r @"packages\MSDN.FSharpChart.dll.0.60\lib\MSDN.FSharpChart.dll"
#r "System.Windows.Forms.DataVisualization.dll"
open System
open System.Windows.Forms
open MSDN.FSharp.Charting

[<Measure>] type g
[<Measure>] type kg
[<Measure>] type ml
[<Measure>] type abv // alcohol by weight
[<Measure>] 
type vol = 
    static member toAbv (x:float<vol>) = x * 0.8<abv/vol>

let gramsPerMilliliters : float<g ml^-1> = 1.0<g/ml>

let calculateAlcoholWeight (amount:float<ml>) (strength:float<vol>) =
    let strengthAbv = vol.toAbv(strength)
    let alcoholWeight = (strengthAbv / 100.0) * amount
    (alcoholWeight * gramsPerMilliliters) / 1.0<abv>

type DrinkType =
    | Beer
    | Wine
    | Cider
    | Drink
    | Spirits

type Gender =
    | Male
    | Feemale
    
type DrinkEntry = { 
    Type:DrinkType; 
    Time:DateTime; 
    Volume:float<ml>; 
    Strength:float<vol>; 
}

type Profile = { 
    Gender:Gender; 
    Weight:float<kg>; 
}

let hoursSince (time:DateTime) =
    DateTime.Now.Subtract(time).TotalHours

let calculateAlcoholLevel (drink:DrinkEntry) (profile:Profile) (time:DateTime) =
    let amountOfAlcohol = calculateAlcoholWeight drink.Volume drink.Strength
    let weightFactor = if(profile.Gender = Gender.Male) then 0.70 else 0.60
    Math.Max(0.0, float (amountOfAlcohol / (profile.Weight * weightFactor)) - (0.15 * time.Subtract(drink.Time).TotalHours))

let profile = { Gender = Gender.Male; Weight = 82.0<kg> };        

let drinks = [
    { Type = DrinkType.Beer; Time = DateTime.Now.AddHours(-2.0); Volume = 500.0<ml>; Strength = 5.0<vol> };
    { Type = DrinkType.Beer; Time = DateTime.Now.AddHours(-1.75); Volume = 500.0<ml>; Strength = 5.0<vol> };
    { Type = DrinkType.Beer; Time = DateTime.Now.AddHours(-1.5); Volume = 500.0<ml>; Strength = 5.0<vol> };
    { Type = DrinkType.Beer; Time = DateTime.Now.AddHours(-1.0); Volume = 500.0<ml>; Strength = 5.0<vol> };
    { Type = DrinkType.Spirits; Time = DateTime.Now.AddHours(-1.0); Volume = 40.0<ml>; Strength = 40.0<vol> };
    { Type = DrinkType.Beer; Time = DateTime.Now.AddHours(-0.25); Volume = 500.0<ml>; Strength = 5.0<vol> };
]

drinks |> Seq.map(fun drink -> calculateAlcoholLevel drink profile DateTime.Now)    
       |> Seq.sum

let startTime = (List.head drinks).Time
let chart = 
    [0..12] 
        |> Seq.map(fun i -> startTime.AddHours(-0.5).AddHours(float i * 0.5))
        |> Seq.map(fun time -> 
            let alcoholLevel = 
                drinks
                |> Seq.filter(fun drink -> drink.Time < time) 
                |> Seq.map(fun drink -> calculateAlcoholLevel drink profile time)
                |> Seq.sum
            (time, alcoholLevel))
        |> Seq.toList
        |> FSharpChart.Line

let form = new Form(Visible = true, TopMost = true, Width = 700, Height = 500)
let ctl = new ChartControl(chart, Dock = DockStyle.Fill)
form.Controls.Add(ctl)
                   