// Code can be organized in namespaces.
namespace demoapp

// The PersonType will be part of the demoapp namespace. 
type PersonType = {First:string; Last:string}

(*  A module is a F# spesific unit of organization.
    A module compiles down to a static class, with any type defined
    as an internal class. Functions are exposed as static methods. *)
module Person =
    type Gender = 
        | Male
        | Female

    let description = "Some static content."

    let create first last =
        {First=first; Last=last}

    let fullName {First=first; Last=last} =
        first + " " + last