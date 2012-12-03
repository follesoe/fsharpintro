#r "FSharp.Data.TypeProviders"
#r "System.Data.Services.Client"

open System
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type Netflix = ODataService<"http://odata.netflix.com/Catalog/">
let data = Netflix.GetDataContext()

let query1 = 
    query {
        for movie in data.Titles do
        where (movie.Name.Contains("American Pie"))
        select movie
    }

query1 |> Seq.iter (fun movie -> printfn "%s" movie.Name)