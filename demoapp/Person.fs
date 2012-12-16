namespace demoapp

module Person =
    type PersonType = {First:string; Last:string}

    type Gender = 
        | Male
        | Female

    let description = "Some static content."

    let create first last =
        {First=first; Last=last}

    let fullName {First=first; Last=last} =
        first + " " + last