#r "FSharp.Data.TypeProviders"
#r "System.Windows.Forms.DataVisualization.dll"
#r @"packages\FSharp.Data.Experimental.1.0.2\lib\net40\FSharp.Data.dll"
#r @"packages\FSharp.Data.Experimental.1.0.2\lib\net40\FSharp.Data.Experimental.dll"
#load "FSharpChart.fsx"

open System.IO
open System.Net
open FSharp.Data

type CurrencyCsv = FSharp.Data.CsvProvider<"historiske_kurser.csv", ",", "en-gb", 10>

let wc = new WebClient()
let data = wc.DownloadString("https://www.dnb.no/portalfront/datafiles/miscellaneous/csv/historiske_kurser.csv")

let exchange = CurrencyCsv.Parse(data)

exchange.Data 
|> Seq.map(fun row -> (row.Dato, row.Usd))
|> Seq.sortBy(fun (date, usd) -> usd)
|> printfn "%A"