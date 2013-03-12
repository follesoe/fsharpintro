(* WIFI: A18EF092DC *)

let str = "Hello World"
let num = 10
let mutable num2 = 20
num2 <- 22

let divisibleBy factor value =
    value % factor = 0

let divisibleByAlt1 (factor) (value) =
    value % factor = 0

let divisibleByAlt2 (factor, value) = 
    value % factor = 0

let funA a b = a + b
let funB = fun a b -> a + b

let makeAdder n = fun i -> n + i
let add5 = makeAdder 5
add5 10

let combine f a b =
    f a b

combine (+) 5 5
combine (-) 5 5
combine (*) 5 5

let (^^) n p = System.Math.Pow(n, p)

2.0 ^^ 8.0
(^^) 2.0 8.0

let (=~=) text pattern =
    System.Text.RegularExpressions.Regex.IsMatch(text, pattern)

"Hello World" =~= "World$"
(=~=) "Hello World" "World$"

let polyFun (a:float) b (c:float) =
    a + c 

let polyFun2 a b c =
    a = c

let val1 = if 10 > 0 then 5 else 10
let val2 = match val1 with
            | 10 -> "Ten"
            | _ -> "Something else.."

let val3 = fun () -> 1
let val4 = try
                1 / 0
           with
            | e -> 100

let emptyList = []
let nums = [1; 2; 3; 4; 5]
let odds = [1; 3; 5; 7; 9]
let evens = [2; 4; 6; 8; 10]

let nums2 = odds @ evens

let nums3 = [1 .. 5]
let tens = [0 .. 10 .. 50]
let countDown = [5 .. -1 .. 0]

let rand = new System.Random()
let randoms = [for x in 1..20 do yield rand.Next(50)]

let numbersNear x =
    [
        yield x - 1
        yield x
        yield x + 1
    ]

numbersNear 10

nums
List.head nums
List.tail nums

let rec sum list =
    match list with
    | [] -> 0
    | hd :: tail -> hd + sum tail 

let rec exists n list =
    match list with
    | [] -> false
    | hd :: tail -> if hd = n then true else exists n tail

exists 5 nums
exists 6 nums

let rec max list =
    match list with
    | [] -> 0
    | hd :: tail -> 
         if hd > max tail then hd else max tail

max nums

let square n = n * n
let cube n = n * n * n
let isEven n = n % 2 = 0
let isOdd n = not (isEven n)

(* squareNums og cubeNums er nesten like - 
   hvordan lage en funksjon som er generisk og kan brukes
   til å implementere squareNums og cubeNums? *)
let rec squareNums nums =
    match nums with
    | [] -> []
    | hd :: tail -> square hd :: squareNums tail

let rec cubeNums nums =
    match nums with
    | [] -> []
    | hd :: tail -> cube hd :: cubeNums tail

squareNums nums
cubeNums nums

(* Hvilken generell funksjon er dette? *)
let rec evenNums nums =
    match nums with
    | [] -> []
    | hd :: tail -> if isEven hd then hd :: evenNums tail
                    else evenNums tail

evenNums nums

let rec map f list = 
    match list with
    | [] -> []
    | hd :: tail -> f hd :: map f tail

map cube nums
map square nums  

let rec filter f list =
    match list with 
    | [] -> []
    | hd :: tail -> if f hd then hd :: filter f tail
                    else filter f tail
                    
filter isEven nums

List.map cube nums
List.map square nums

List.filter isEven nums
List.filter isOdd nums

List.map cube (List.filter isOdd (List.map square nums))
(isEven (square 512))

nums 
|> List.map square 
|> List.filter isOdd 
|> List.map cube



let myIsEven = divisibleBy 2
myIsEven 2
myIsEven 3

(* (|>) a f = f a *)

512 |> square |> isEven
512 |> square

(|>) 512 square

let myMap = List.map square

myMap nums

(* gitt f og g, og verdien a, 
   regn f a, og send den g
   hvis du kun gir f og g
    g(f(a)) *)

let cubeTheSquare = square >> cube
let cubetheSquare2 = cube << square

cubeTheSquare 2
cubetheSquare2 2

let describeNum n =
    match n with
    | 0 -> "None"
    | 1 -> "One"
    | 2 -> "Two"
    | _ when n < 0 -> "Confused..."
    | _ -> "Many"

let describeOption o = 
    match o with
    | None -> "Nothing.."
    | Some 42 -> "The answer!"
    | Some n -> sprintf "Some value: %A" n    

let describeList list =
    match list with
    | [] -> "Empty list.."
    | [_] -> "One element.."
    | [_;_] -> "Two elemenets.."
    | hd :: tail -> "More than two elements..."


type Point = (int * int)

let pointA = (1, 1)
let pointB:Point = (1,2)



type Person = { Age:int; Name:string }

let me = { Age = 29; Name = "Jonas" }
let olderMe = { me with Age = 30 }
let oldMe = { me with Age = 50 }

let describePerson person = 
    match person with
    | { Age = 30; Name = name } -> sprintf "You are getting old %s" name
    | { Age = age; Name = name } when age < 30 -> sprintf "You are stil yong %s" name
    | { Age = _; Name = name } -> sprintf "You are old %s" name


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

let describeCards cards =
    match cards with
    | []
    | [_] -> "Too few cards."
    | cards when List.length cards > 2 -> "Too many cards."
    | [ Ace(_); Ace(_) ] -> "Pocket Rockets"
    | [ King(_); King(_) ] -> "Cowboys"
    | [ ValueCard(2, _); ValueCard(2, _) ] -> "Ducks"
    | [ Queen(_); Queen(_)]
    | [ Jack(_); Jack(_)] -> "Pair of face cards"
    | [ ValueCard(x, _); ValueCard(y, _) ] when x = y -> "A pair"
    | [ first; second] -> sprintf "Two card: %A and %A" first second
    | _ -> "Error" 


[<Measure>] type kg
[<Measure>] type s
[<Measure>] type h
[<Measure>] type m
[<Measure>] type km

let gravity = 9.81<m/s^2>
let height = 3.5<m>

let speed = sqrt (2.0 * gravity * height)

let kmToM = 1000.0<m/km>
let hrToSec = 3600.0<s/h>

let msToKmph(speed: float<m/s>) =
    speed / kmToM * hrToSec
