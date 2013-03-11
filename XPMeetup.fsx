module XPMeetup

open System

(* F# is a statically typed language *)

let str = "Hello World"
let num = 10

(* ..but we do not have to speficy types everywere. 
   We define functions the same way we introduce value binding.
   Infact, a function is just a function-value bound to a name. *)

let divisibleBy factor value =
    value % factor = 0 // 0 is an int, so factor and value must be ints
    
let divisibleByAlt1 (factor) (value) = // Parentheses are optional 
    value % factor = 0

let divisibleByAlt2 (factor: int) (value: int) = // We only use them to provide type hints.
    value % factor = 0
    
(* Every F# function takes a single argument, 
   so this function takes a single (int * int) tupple arg.
   This enables some interesting programming techniques that we will explore later. *)
let divisibleByAlt3 (factor, value) =
    value % factor = 0               
    
(* Variables are immutable by default in F#, so we
   call them value-bindings instead of variables *)
   
// let num = 20 // Will cause a "Duplicate definition of value 'num'-error.

let mutable num2 = 10 // Can mark a variable as mutable.
num2 <- 20

(* Since F# functions are standard value bindings,
   these two definitions are equivalent *)
let funA a b = a + b
let funB = fun a b -> a + b

let resA = funA 5 5
let resB = funB 5 5

(* 
   F# has first-class functions. 
*)
let makeAdder n = fun i -> n + i // ... functions can return new functions
let add5 = makeAdder 5 // ... we can assign functions to variables (value binding)
add5 10 // ... evaluates to 15.

// .. and we can pass functions to other functions.
let combine f a b =
    f a b

combine (+) 5 5
combine (*) 5 5
combine (-) 5 5

(* In F# +, * and - are standard functions. We can define our own
   infix operators (functions) as well *)
let (^^) n p = System.Math.Pow(n, p)

let (=~=) text pattern = 
    System.Text.RegularExpressions.Regex.IsMatch(text, pattern)

2.0 ^^ 8.0
(^^) 2.0 8.0 // Can be called just like any other function... will be important later.

"Hello World" =~= "World$"
(=~=) "Hello World" "World$"

(* F# Type Inference supports polymorphic functions *)
let polyFun1 a b c =
    a + c
    
let polyFun2 a b c =
    a = c
    
(* In F#, white-space is significant, and we do not have explicit return statements *)

(* In F# everything is an expression.
   An "expression" is a combination of values and functions that are combined to create a new value,
   opposed to a "statement" which is just a standalone unit of execution and doesn't return anything.
   The purpose of an expression is to compute a value (with some possible side-effect), while the 
   sole purpose of a statement is to have side-effects.
   
   In F#, even loops, pattern-matching and control-structures are expressions.
*)

let val1 = if 10 > 0 then 5 else 10

let val2 = match val1 with
            | 10 -> "Ten"
            | _ -> "Something else..."

let val3 = fun () -> 1

let val4 = try 
                1 / 0
            with
                | e -> 100                

(* Lists are defined using the list literal syntax... *)

let emptyList = []
let nums = [1; 2; 3; 4; 5]
let odds = [1; 3; 5; 7; 9]
let evens = [2; 4; 6; 8; 10]

let nums2 = odds @ evens // @-operator concatinates lists

(* ... or they can be defined using ranges *)
let nums3 = [1 .. 5]
let tens = [0 .. 10 .. 50]
let countDown = [5 .. -1 .. 0]

(* ... or using List comprehensions *)

let rand = Random()
let randoms = [ for x in 1..20 do yield rand.Next(50) ]

let numbNear x =
    [
        yield x - 1
        yield x
        yield x + 1
    ]

(* We can access lists using head/tail/nth *)
List.head nums
List.tail nums
List.nth nums 3

(* In Functional Programming we use recursion to process lists.
   This prevents us from having to mutate state. We often end up with a more
   declerative algorithm. *)

let rec sum list = 
    match list with 
    | [] -> 0
    | hd :: tail -> hd + sum tail

let rec exists n list =
    match list with
    | [] -> false
    | hd :: tail -> if hd = n then true else exists n tail

let rec max list = 
    match list with 
    | [] -> 0
    | hd :: tail -> if hd > max tail then hd else max tail

(* Processing lists is a common task in any programming language.
   In FP the pattern of list processing ofen become clear, and we can extract
   the spesific part of an algoritm from the generic. The spesific part can be passed in 
   as an higher order function *)

let square n = n * n
let cube n = n * n * n
let isEven n = n % 2 = 0
let isOdd n = not (isEven n)

let rec squareNums nums =
    match nums with
    | [] -> []
    | hd :: tail -> square hd :: squareNums tail

let rec cubeNums nums =
    match nums with
    | [] -> []
    | hd :: tail -> cube hd :: cubeNums tail

let rec evenNums nums =
    match nums with
    | [] -> []
    | hd :: tail -> if isEven hd then hd :: evenNums tail 
                    else evenNums tail

let rec oddNums nums =
    match nums with
    | [] -> []
    | hd :: tail -> if isOdd hd then hd :: oddNums tail 
                    else oddNums tail

(* Assignment: what is the generic part of the functions above?
   Create a new function with an appropiate name, that takes a higher-order function
   for the spesific part of the function *)

(* The function f is the spesific part, map is the generic part *)

let rec map f list = 
    match list with
    | [] -> []
    | hd :: tail -> f hd :: map f tail

let rec filter f list = 
    match list with
    | [] -> []
    | hd :: tail -> if f hd then hd :: filter f tail else filter f tail

(* Most of the common list processing functions are available as higher-order functions *)                   
List.map square nums
List.map cube nums                    
List.filter isOdd nums
List.filter isEven nums

(* Syntax becomes clunky when chaining higher-order function calls like this *)
List.filter isOdd (List.map square nums)

nums |> List.map square |> List.filter isOdd // Can use pipe-forward operator

(* let (|>) a f = f a *)

(isEven (square 512))
512 |> square |> isEven

(* Currying is the fact that any function taking n-arguments can be 
   turned into a chain of n-function calls taking 1 argument. In F#
   currying is built in, and be used on any function. Passing some, but not all
   arguments is called partial function application *)

let myIsEven = divisibleBy 2
let isFizz = divisibleBy 3
let isBuzz = divisibleBy 5

let myMap = List.map square
let myFilter = List.filter isEven

nums |> myMap
nums |> myFilter

(|>) nums myMap
(|>) nums myFilter

nums |> List.map square |> List.filter isOdd

(* Have other function composition operators *)

(*  Function composition using << or >>
    Given two functions, f and g and a value a, compute
    f of a and pass that to g. By passing in only f and g, you can
    create a function computing g(f(a)) *)
let cubeTheSquare = square >> cube
let cubeTheSquare2 = cube << square

(* Pattern matching is a key technique to deconstruct data *)

let describeNum n =
    match n with
    | 0 -> "None"
    | 1 -> "One"
    | 2 -> "Two"
    | _ when n < 0 -> "Confused..."
    | _ -> "Many..."

let describeOption o =
    match o with
    | None -> "Nothing..."
    | Some 42 -> "The answer to everything!"
    | Some n -> sprintf "Some value: %A" n

let describeList list = 
    match list with
    | [] -> "Empty list..."
    | [_] -> "One element list..."
    | hd :: tail -> "More than one element..."

let describeNumbers a b =
    match a, b with
    | _, 1
    | 1, _ -> "One of the numbers are 1"
    | 2, 2 -> "Both numbers are 2"
    | _ -> "Other..."

(* Working with data types *)

(* Type synonyms are the simplest form of data type *)
type Point = (int * int)

(* Records are simple immutable, data records *)

type Person = { Age:int; Name:string; }

let me = { Age=29; Name ="Jonas" }
let olderMe = { me with Age = 30 }
let oldMe = { me with Age = 50 }

let describePerson person = 
    match person with
    | { Age = 30; Name = name } -> sprintf "You are getting old %s" name
    | { Age = age; Name = name } when age < 30 -> sprintf "You are still young %s" name
    | { Age = _; Name = name } -> sprintf "You are old %s" name

(* Discriminated unions *)
type Suit =
    | Heart
    | Diamond
    | Spade
    | Club

type PlayingCard = 
    | Ace of Suit
    | King of Suit
    | Queen of Suit
    | Jack of Suit
    | ValueCard of int * Suit

(* 
   Assignment: Create a function taking in a list of cards, that returns 
   a string describing two opening cards in Poker. If the list has fewer than 2 elements,
   the function should return a message saying Two few card. If the list has more than 2 elements,
   the function should return a message saying too many cards. Else the following rules apply:

   - Two ace: "Pocket Rockets"
   - Two kings: "Cowboys"
   - Two value cards of 2: "Ducks"
   - Two qeens or two jacks: "Pair of face cards"
   - Two value cards of same value: "A pair"
   - Else: "Two cards; A and B" (with A and B being the cards)
*)


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
    | _ -> sprintf "Error..."