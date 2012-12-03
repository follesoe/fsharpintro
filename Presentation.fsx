
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

// Type inference example
let mult1 x y = x * y // x:int -> y:int -> int
let mult2 x y = x * y // x:float -> y:float -> float

let res1 = mult1 4 5
let res2 = mult2 4.0 5.0

// if-expressions return a value
let isEven x =
    let result =
        if x % 2 = 0 then "Yes it is"
        else "Not is it not"
    result

isEven 1
isEven 2

// Tuples
let dinner = ("pizza", "taco")
let item1 = fst dinner
let item2 = snd dinner
let food1, food2 = dinner

let tupledAdd(x, y) = x + y
tupledAdd(3, 7)

// Lists
let fruits = ["apple"; "pear"; "banana"]
let moreFruits = "pineapple" :: fruits
let fruitsAndVegetables = ["potato"; "cabbige"] @ fruits

// Ranges
let oneToTen = [1 .. 10]