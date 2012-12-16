namespace demoapp

open System

module Program =
    [<EntryPoint>]
    let main argv = 

        let person = Person.create "Jonas" "Follesø"
        Person.fullName person |> printfn "%s"

        let customer = new Customer("Apple", new DateTime(2000, 1, 1))
        printfn "%s is a prefered customer: %b" customer.Name customer.IsPreferedCustomer 
        0 // return an integer exit code        