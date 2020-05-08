open System
open System.IO

[<EntryPoint>]
let main argv =
    let calcFuel mass = (mass/3) - 2
    
    let unfoldFuel fuel =
        match (calcFuel fuel) with
        | requiredFuel when requiredFuel > 0 -> Some(requiredFuel, requiredFuel)
        | _ -> None

    let findRequiredFuel mass = Seq.unfold unfoldFuel mass

    let lines = File.ReadAllLines("input.txt")
    let total = 
        lines
            |> Seq.map (fun l -> Int32.Parse l |> findRequiredFuel)
            |> Seq.concat
            |> Seq.sum
    printfn "%i" total
    0 // return an integer exit code
