
(* 
Factorial - The Hello World of 
Functional Programming
*)

let rec fac n =
    if n <= 1 then 1
    else n * fac(n - 1)

fac 5

let rec fac2 n =
    match n with
    | 0 -> 1
    | n -> n * fac2(n - 1)

fac2 5

// Compute the greates common divisor of two numbers.
let rec gcd x y =
    if y = 0 then x
    else gcd y (x % y)

gcd 1024 12

// Compute the nth fibonacci number
let rec fib n =
    match n with
    | 0 -> 1
    | 1 -> 1
    | n -> fib(n - 1) + fib(n - 2)

fib 7