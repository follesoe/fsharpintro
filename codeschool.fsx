open System
open System.IO
open System.Net
open System.Text.RegularExpressions;

let startDate = new DateTime(2013, 2, 1)
let users = ["follesoe"; "lillesand"; "joelchelliah"; "frodereinertsen"]

type ReportCard = { Username : string; Courses : (string * DateTime) list}

let fetchAsync username =
    async {
        let uri = new Uri(sprintf "http://www.codeschool.com/users/%s" username)
        let webClient = new WebClient()
        let! html = webClient.AsyncDownloadString(uri)
        return (username, html)
    }

let parseCourse (course:string) =
    let time = Regex.Match(course, @"(?<=\bdatetime="")[^""]*").Value
    let course = Regex.Match(course, @"(?<=\balt="")[^""]*").Value    
    (course, Convert.ToDateTime(time))

let extractReportCard (username, html) =    
    let matches = Regex.Matches(html, "<li class='course card-c alt course-complete'>((?!</li>).|\n)+</li>")
    let courses = matches 
                    |> Seq.cast 
                    |> Seq.map (fun (m:Match) -> parseCourse m.Value)
                    |> Seq.filter (fun (_, date) -> date >= startDate)
                    |> Seq.toList
    { Username = username; Courses = courses }

let printCard card =
    printfn "%s (%d courses)" card.Username card.Courses.Length
    card.Courses |> Seq.iter (fun (name, date) -> printfn "- %s (%A)" name date)
    printfn ""

let printList() =
    users
    |> Seq.map fetchAsync
    |> Async.Parallel
    |> Async.RunSynchronously
    |> Seq.map extractReportCard
    |> Seq.sortBy (fun (card) -> card.Courses.Length * -1)
    |> Seq.iter printCard

printList()