#r "System.ServiceModel"
#r "FSharp.Data.TypeProviders"

open System
open System.ServiceModel
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type tempService = WsdlService<"http://www.w3schools.com/webservices/tempconvert.asmx?wsdl">
let tempClient = tempService.GetTempConvertSoap();
let far = tempClient.CelsiusToFahrenheit("25")

printfn "25 celcius is %s farenheit" far