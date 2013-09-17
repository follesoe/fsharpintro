open System
open System.Text.RegularExpressions

(*  We are not specifying types.
    F# uses type inference to figure out the type *)
let num = 123
let str = "Hello World"

(* F# is Immutable by default. *)
let x = 1
let x = 2
let x = 3
let x = "abc"

(* Must explicitly make a variable mutable *)
let mutable y = 1
y <- 2
y <- 3

(* Define functions and partial function application *)

let divisibleBy factor value =
    value % factor = 0

let isEven = divisibleBy 2

[1..10] |> Seq.filter(isEven) |> Seq.iter (printfn "%d")

(* Pattern matching ... *)

// ... on constants
let count n =
    match n with
    | 0 -> "none"
    | 1 -> "one"
    | 2 -> "two"
    | _ when n < 0 -> "confused" // When guard.
    | _ -> "many"

// Pattern matching on discriminated union (Options)
let describeOption o =
    match o with
    | Some(42)  -> "The answer to everything!"
    | Some(x)   -> sprintf "The answer was %d" x
    | None      -> "No answer."

// List comprehensions
let sampleNumbers = [ 0 .. 99 ]
let rand = Random()
let randoms = [ for x in 1..20 do yield rand.Next(50) ]

// Records and higher-order functions
type Person = { Age:int; Name:string; Twitter:string }

let people = [
    { Age = 29; Name = "Jonas"; Twitter = "follesoe" };
    { Age = 20; Name = "Bob"; Twitter = "bob" };
    { Age = 25; Name = "Joe"; Twitter = "joe" };
    { Age = 29; Name = "Jonas"; Twitter = "follesoe" };
    { Age = 50; Name = "Jane"; Twitter = "" };
]

let following = 
    people 
    |> Seq.filter(fun person -> person.Twitter <> "")
    |> Seq.sortBy(fun person -> person.Age)
    |> Seq.map(fun person -> person.Twitter)
    |> Seq.distinct
    |> Seq.toList