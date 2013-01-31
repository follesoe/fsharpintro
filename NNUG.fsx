module Demo

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

open System

(*  Define function bindings using the let keyword.
    Do not specify the types. 
    White space significant. 
    No explicit return - everything is an expression. *)
let pythagoras x y =
    Math.Sqrt(x * x + y * y)

let divisibleBy factor value =
    value % factor = 0 // 0 is an int, so factor and value must also be ints.

let divisibleByAlt1 (factor) (value) = // Parentheses are optional
    value % factor = 0

let divisibleByAlt2 (factor, value) = // This is a single tuple argument, not two arguments.
    value % factor = 0

let divisibleByAlt3 (factor:int) (value:int) = // Can provide type annotations to arguments
    value % factor = 0

(*  Type aliasing *)
type Username = string
type Predicate<'a> = 'a -> bool
type Coordinate = int*int

let swap (coord:Coordinate) =
    (snd coord, fst coord)

let auth (username:Username) (pred:Predicate<Username>) =
    if pred(username) then true else false

let checkForRoot username = 
    username = "root"    

auth "follesoe" checkForRoot
auth "root" checkForRoot
 
(* Use the rec keyword to tell the F# Type Inference system
   this is a recursive function. *)
let rec fact n =
    if n <= 1 then 1 else n * fact (n-1) // If is an expression with a value.

(* You can add new infix operators. 
   Brackets around function name tells F# it is an operator *)
let (^^) n p = Math.Pow(n, p)

let (=~=) text pattern = 
    System.Text.RegularExpressions.Regex.IsMatch(text, pattern)

// Pattern matching
let count n =
    match n with
    | 0 -> "none"
    | 1 -> "one"
    | 2 -> "two"
    | _ when n < 0 -> "confused" // When guard.
    | _ -> "many"

// Pattern matching on list
let rec listLength l =
    match l with
    | []        -> 0
    | [_]       -> 1
    | [_;_]     -> 2
    | [_;_;_]   -> 3
    | head :: tail -> 1 + listLength tail

// Map implemented using recursion and pattern matching
let rec myMap list f =
    match list with
    | []    -> []
    | head :: tail -> f(head) :: myMap tail f

// Pattern matching on discriminated union (Options)
let describeOption o =
    match o with
    | Some(42)  -> "The answer to everything!"
    | Some(x)   -> sprintf "The answer was %d" x
    | None      -> "No answer."

// Tuples
let nameTuple = ("Jonas", "Follesø")
let fname = fst nameTuple
let lname = snd nameTuple

// Lett bindings are actually pattern-match rules
let firstname, lastname = nameTuple

// .. is the same as
match nameTuple with
| fname, lname -> sprintf "Hello %s %s" fname lname

// List literals
let smallOddPrimes = [3; 5; 7] // Object.ReferenceEquals(smallPrimes.Tail, smallOddPrimes);;

// List construction (cons)
let smallPrimes = 2 :: smallOddPrimes // Creates a new list. Show with obj. equals.

// List append
let list1 = [2; 3; 4]
let list2 = [5; 6; 7;]
let list3 = list1 @ list2

// List comprehensions
let sampleNumbers = [ 0 .. 99 ]

let rand = Random()
let randoms = [ for x in 1..20 do yield rand.Next(50) ]

// Operating over lists with higer order functions
let sequared nums = List.map (fun x -> x * x) nums
let even nums = List.filter (fun x -> divisibleBy 2 x) nums

// Composing list operators
let sequaredEvens_bad nums = 
    List.map (fun x -> x * x) (List.filter (fun x -> divisibleBy 2 x) nums)

// Pipe-forward operator
let sequaredEvens nums = 
    nums 
    |> List.filter(divisibleBy 2) // Partial function application
    |> List.map (fun x -> x * x)

let squaredOdds nums =
    nums
    |> List.filter(not << divisibleBy 2) // Function composition
    |> List.map(fun x -> x * x)
    
(*  Defined as: let (|>) x f = f x
    Given a generic type 'a, and a function which takes 'a and returns 'b,
    then return the application of the function of the input. *)

let square x = x * x
let toStr (x : int) = x.ToString()
let rev (x : string) = 
    let letters = x.ToCharArray()
    new String(Array.rev(letters))

let result = rev (toStr (square 512))
let result2 = 512 |> square |> toStr |> rev

(*  F# suports currying - the ability to transform a 
    function taking n arguments, into a chain of n functions,
    each taking one argument. *)

let divisibleByTwo = divisibleBy 2

(*  Function composition using << or >>
    Given two functions, f and g and a value a, compute
    f of x and pass that to g. By passing in only f and g, you can
    create a function computing g(f(a)) *)
let reversedSquareStr = square >> toStr >> rev
let reversedSquareStr2 = rev << toStr << square 

reversedSquareStr 10
reversedSquareStr2 10

// Define a record
type Person = { Age:int; Name:string; Twitter:string }

// Type inference of record
let me = { Age = 29; Name = "Jonas"; Twitter = "follesoe" }
let me2 = { Age = 29; Name = "Jonas"; Twitter = "follesoe" }
let olderMe = { me with Age = 30 } // Cloning.

// Pattern matching on records
let printAge person =
    match person with 
    | { Age = 30; Name = _; Twitter = _ } -> "You are getting old..."
    | { Age = 29; Name = _; Twitter = _ } -> "Still one year left..."
    | { Age = age; Name = _; Twitter =_ } -> sprintf "You are %A years old..." age

// Deconstruction of record using let binding (pattern matching)
let { Twitter = twitter } = me

// Property access of record members
let name = me.Name

// Discriminated unions
type Errorable<'a> =
    | Success of 'a
    | Error of string

(* A discriminated union is a type that can s
   be one of a set of possible values. Each possible
   value is referred to as a union case *)

type Suit =
    | Heart
    | Diamond
    | Spade
    | Club

// Discriminated Union with member.
type PlayingCard = 
    | Ace of Suit
    | King of Suit
    | Queen of Suit
    | Jack of Suit
    | ValueCard of int * Suit
    member this.Value =
        match this with
        | Ace(_) -> 14
        | King(_) -> 13
        | Queen(_) -> 12
        | Jack(_) -> 11
        | ValueCard(value, _) -> value

// List comprehension creating a playing deck.
let deckOfCards = [
    for suit in [ Spade; Club; Heart; Diamond ] do
        yield Ace(suit)
        yield King(suit)
        yield Queen(suit)
        yield Jack(suit)
        for value in 2 .. 10 do yield ValueCard(value, suit)
]

// Pattern matching on list of Discriminated Unions
let describeHoleCards cards = 
    match cards with
    | []
    | [_] -> failwith "Too few cards."
    | cards when List.length cards > 2 -> failwith "Too many cards."
    | [ Ace(_); Ace(_) ] -> "Pocket Rockets"
    | [ King(_); King(_) ] -> "Cowboys"
    | [ ValueCard(2, _); ValueCard(2, _)] -> "Ducks"
    | [ Queen(_); Queen(_) ]
    | [ Jack(_); Jack(_) ] -> "Pair of face cards"
    | [ ValueCard(x, _); ValueCard(y, _) ] when x = y -> "A Pair"
    | [ first; second ]  -> sprintf "Two cards: %A and %A" first second


(*  Queries and F# Query Expressions aka LINQ
    You describe a query in a dclarative manner, rather than 
    relying on a step-by-step sequence pipeline. This allows
    new capabilities such as converting program code to a SQL query.
 *)
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
      
let following2 = query {
    for person in people do
    where (person.Twitter <> "")
    sortBy (person.Age)
    select person.Twitter
    distinct
}