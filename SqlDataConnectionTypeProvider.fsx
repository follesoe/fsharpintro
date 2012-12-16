#r "System.Data.dll"
#r "FSharp.Data.TypeProviders.dll"
#r "System.Data.Linq.dll"

open System
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

type dbSchema = SqlDataConnection< @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\source\fsharpintro\packages\Northwind.Db.1.0.0\content\App_Data\Northwind.MDF;Integrated Security=True;Connect Timeout=30" >
let db = dbSchema.GetDataContext()

// Enable the logging of database activity to the console.
db.DataContext.Log <- System.Console.Out

let query1 =
        query {
            for row in db.Employees do
            where (row.Country = "UK")
            select row
        }
query1 |> Seq.iter (fun row -> printfn "%s %s %s" row.FirstName row.LastName row.Country)

let query2 = query { for row in db.Customers do select row }

query2 |> Seq.iter (fun row -> printfn "%s %s" row.CompanyName row.ContactName)