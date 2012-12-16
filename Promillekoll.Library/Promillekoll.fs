namespace Promillekoll.Library

open System

[<Measure>] type g
[<Measure>] type kg
[<Measure>] type ml
[<Measure>] type abv
[<Measure>] 
type vol = 
    static member toAbv (x:float<vol>) = x * 0.8<abv/vol>

type Gender =
    | Male      = 1
    | Feemale   = 2

type DrinkType =
    | Beer      = 0
    | Wine      = 1
    | Cider     = 2
    | Drink     = 3
    | Spirits   = 4

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

module Promillekoll =
    let private gramsPerMilliliters : float<g ml^-1> = 1.0<g/ml>

    let private calculateAlcoholWeight (amount:float<ml>) (strength:float<vol>) =
        let strengthAbv = vol.toAbv(strength)
        let alcoholWeight = (strengthAbv / 100.0) * amount
        (alcoholWeight * gramsPerMilliliters) / 1.0<abv>

    let private hoursSince (time:DateTime) =
        DateTime.Now.Subtract(time).TotalHours

    let private calculateAlcoholLevelForDrink (drink:DrinkEntry) (profile:Profile) (time:DateTime) =
        let amountOfAlcohol = calculateAlcoholWeight drink.Volume drink.Strength
        let weightFactor = if(profile.Gender = Gender.Male) then 0.70 else 0.60
        Math.Max(0.0, float (amountOfAlcohol / (profile.Weight * weightFactor)) - (0.15 * time.Subtract(drink.Time).TotalHours))

    let calculateCurrentAlcoholLevelAt profile drinks time = 
        let alcoholLevel = 
            drinks
            |> Seq.filter(fun drink -> drink.Time < time) 
            |> Seq.map(fun drink -> calculateAlcoholLevelForDrink drink profile time)
            |> Seq.sum
        alcoholLevel

    let calculateAlcoholLevelOverTime profile drinks =
        let startTime = (List.head drinks).Time
        let values = 
            [0..12] 
                |> Seq.map(fun i -> startTime.AddHours(-0.5).AddHours(float i * 0.5))
                |> Seq.map(fun time -> (time, calculateCurrentAlcoholLevelAt profile drinks time))
                |> Seq.toList 
                |> List.toSeq
        values