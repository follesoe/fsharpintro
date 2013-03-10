module XPMeetup

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

(* F# has first-class functions. *)
let makeAdder n = fun i -> n + i // ... functions can return new functions
let add5 = makeAdder 5 // ... we can assign functions to variables (value binding)
add5 10 // ... evaluates to 15.

// .. and we can pass functions to other functions.
let rec myMap f list = 
    if List.isEmpty list then []
    else f(List.head list) :: myMap f (List.tail list)
    
myMap add5 [1;2;3] // [6; 7; 8]



