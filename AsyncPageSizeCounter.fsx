open System
open System.IO
open System.Net

let urls = [ 
    "https://raw.github.com/follesoe/fsharpintro/master/AsyncPageSizeCounter.fsx"; 
    "https://raw.github.com/follesoe/fsharpintro/master/WsdlServiceTypeProvider.fsx" ]

let printResult (url:string) (result:string) = 
    printfn "Size of %s is %i" url result.Length

let fetchAsync (url:string) =
    async {
        let uri = new Uri(url)
        let webClient = new WebClient()
        let! html = webClient.AsyncDownloadString(uri)
        return html
    }

let download =
    urls 
    |> Seq.map fetchAsync
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.reduce (+)
    |> printfn "%s"
    |> ignore