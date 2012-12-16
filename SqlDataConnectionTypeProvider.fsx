#r "System.Data.dll"
#r "FSharp.Data.TypeProviders.dll"
#r "System.Data.Linq.dll"

open System
open System.Linq
open System.Data
open System.Data.Linq
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq

type dbSchema = SqlDataConnection< @"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\source\fsharpintro\packages\Northwind.Db.1.0.0\content\App_Data\Northwind.MDF;Integrated Security=True;Connect Timeout=30" >
let db = dbSchema.GetDataContext()

// Enable the logging of database activity to the console.
db.DataContext.Log <- System.Console.Out

let ukCustomers =
        query {
            for row in db.Employees do
            where (row.Country = "UK")
            select row
        }
ukCustomers |> Seq.iter (fun row -> 
    printfn "%s %s %s" row.FirstName row.LastName row.Country)

let allCustomers = query { 
    for row in db.Customers do 
    select (row.CompanyName, row.ContactName) 
}

allCustomers |> Seq.iter (fun row -> 
    let (companyName, contactName) = row
    printfn "%s Contact: %s" companyName contactName)

let customersByCountry = 
    query {
        for customer in db.Customers do
        groupBy customer.Country into g
        sortByDescending (g.Count())
        select (g.Key, g.Count())
    } 
    |> Seq.iter(fun c ->
            let (country, count) = c
            printfn "%s: %A customers(s)" country count)