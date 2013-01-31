#r "FSharp.Data.TypeProviders"
#r "System.Windows.Forms.DataVisualization.dll"
#r @"packages\FSharp.Data.Experimental.1.0.2\lib\net40\FSharp.Data.dll"
#r @"packages\FSharp.Data.Experimental.1.0.2\lib\net40\FSharp.Data.Experimental.dll"
#r @"packages\MSDN.FSharpChart.dll.0.60\lib\MSDN.FSharpChart.dll"

open System.IO
open System.Net
open FSharp.Data
open System.Windows.Forms
open MSDN.FSharp.Charting

type CurrencyCsv = FSharp.Data.CsvProvider<"historiske_kurser.csv", ",", "en-us", 10>

let wc = new WebClient()
let data = wc.DownloadString("https://www.dnb.no/portalfront/datafiles/miscellaneous/csv/historiske_kurser.csv")

let exchange = CurrencyCsv.Parse(data)

exchange.Data 
|> Seq.map(fun row -> (row.Dato, row.Usd))
|> Seq.sortBy(fun (date, usd) -> usd)
|> printfn "%A"

(*
let form = new Form(Visible = true, TopMost = true, Width = 700, Height = 500)
let ctl = new ChartControl(chart2, Dock = DockStyle.Fill)
form.Controls.Add(ctl)
*)