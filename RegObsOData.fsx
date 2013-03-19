#r "FSharp.Data.TypeProviders"
#r "System.Data.Services.Client"

open System
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type RegObs = ODataService<"http://api.nve.no/hydrology/regobs/v0.8.7/Odata.svc">
let data = RegObs.GetDataContext()

let ava = query {
    for a in data.AvalancheObs do
    where (a.Comment.Length > 0)
    sortByDescending a.DtAvalancheTime
    take 10
    select a
}

ava |> Seq.iter(fun a -> printfn "(%A): %s" a.DtAvalancheTime a.Comment)