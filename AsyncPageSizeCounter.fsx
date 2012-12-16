open System
open System.IO
open System.Net

let urls = [ "http://www.vg.no"; "http://www.db.no" ]

let printResult (url:string) (result:string) = 
    printfn "Size of %s is %i" url result.Length

let fetchAsync (url:string) =
    async {
        let uri = new Uri(url)
        let webClient = new WebClient()
        let! html = webClient.AsyncDownloadString(uri)
        printResult url html
    }

let download =
    urls 
    |> Seq.map fetchAsync
    |> Async.Parallel
    |> Async.RunSynchronously
    |> ignore