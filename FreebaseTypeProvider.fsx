﻿#r "FSharp.Data.TypeProviders"
#r @"packages\FSharpx.TypeProviders.Freebase.1.6.122\lib\40\Fsharpx.TypeProviders.Freebase.dll"

let data = FSharpx.TypeProviders.Freebase.FreebaseData.GetDataContext()

// Print the chemical elements.
let elems = data.``Science and Technology``.Chemistry.``Chemical Elements``

elems
|> Seq.filter(fun e -> e.``Atomic number``.HasValue)
|> Seq.sortBy(fun e -> e.``Atomic number``.GetValueOrDefault())
|> Seq.iter(fun e -> printfn "%s\t%s" e.Symbol e.Name)

// Print the US presidents sorted by height
let presidents = 
    query {
        for president in data.Society.Government.``US Presidents`` do
        where president.Height.HasValue
        sortByDescending president.Height.Value
        select (president.Height, president.Name)
    } |> Seq.toList

presidents |> Seq.iter(fun p ->     
                let height, name = p
                printfn "%A %s" height.Value name)

// Print functional programming languages
(*
let computers = data.``Science and Technology``.Computers

let languages = 
    query {
        for language in computers.``Programming Languages`` do
        for paradigm in language.``Language Paradigms`` do
        select (language.Name, language.``Language Designers``, language.``Language Paradigms``)
    } |> Seq.toList
*)