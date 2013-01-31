﻿#r "FSharp.Data.TypeProviders"
#r @"packages\FSharp.Data.1.0.13\lib\net40\FSharp.Data.dll"
#load "FSharpChart.fsx"

open System
open FSharp.Data
open Samples.FSharp.Charting

let data = WorldBank.GetDataContext()

[data.Countries.Sweden.Indicators.``Adjusted net national income (current US$)`` |> Chart.Line;
data.Countries.Norway.Indicators.``Adjusted net national income (current US$)`` |> Chart.Line] |> Chart.Combine