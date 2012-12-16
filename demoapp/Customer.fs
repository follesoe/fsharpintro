namespace demoapp

open System

type Customer =
    val Name : string
    val CustomerSince : DateTime

    new (name, customerSince) = 
        { Name = name; CustomerSince = customerSince }

    new () = 
        { Name = ""; CustomerSince = DateTime.MaxValue }

    member this.IsPreferedCustomer =
        DateTime.Today.Subtract(this.CustomerSince).TotalDays > 365.0 * 10.0