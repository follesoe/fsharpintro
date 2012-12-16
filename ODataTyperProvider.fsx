#r "FSharp.Data.TypeProviders"
#r "System.Data.Services.Client"

open System
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type Netflix = ODataService<"http://odata.netflix.com/Catalog/">
let data = Netflix.GetDataContext()

let topMovies = query {
    for movie in data.Titles do
    where (movie.Name.Contains "Christmas")
    where (movie.ReleaseYear ?> 2000)
    sortByNullableDescending movie.AverageRating
    take 5
    select movie
}

topMovies |> Seq.iter (fun m -> 
                printfn "%A: %s (%A)" m.AverageRating m.Name m.ReleaseYear)