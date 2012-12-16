#r "System.ServiceModel"
#r "FSharp.Data.TypeProviders"

open System
open System.ServiceModel
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Data.TypeProviders

type tempService = WsdlService<"http://www.w3schools.com/webservices/tempconvert.asmx?wsdl">
let tempClient = tempService.GetTempConvertSoap();

tempClient.CelsiusToFahrenheit("-25")