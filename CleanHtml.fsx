#r @"packages\HtmlAgilityPack.1.4.6\lib\Net45\HtmlAgilityPack.dll"
#r "System.ServiceModel.dll"
#r "System.Runtime.Serialization.dll"

open System
open System.Net
open System.Web
open System.Xml
open System.ServiceModel.Syndication

open HtmlAgilityPack

let reader = XmlReader.Create("http://gemini.no/delmed/del_sintef/feed/")
let syndication = SyndicationFeed.Load(reader)

let extractHtml (item:SyndicationItem) =
    let extension = item.ElementExtensions.ReadElementExtensions<string>("encoded", "http://purl.org/rss/1.0/modules/content/")
    extension |> Seq.head

let cleanHtml html =
    let htmlDoc = new HtmlDocument();    
    htmlDoc.LoadHtml(html)
    let nodes = htmlDoc.DocumentNode.SelectNodes("//p")
    if nodes <> null then nodes |> Seq.iter(fun (n) -> n.Remove()) |> ignore
    htmlDoc.DocumentNode.InnerHtml

syndication.Items |> Seq.map(extractHtml) |> Seq.map(cleanHtml) |> printfn "%A"