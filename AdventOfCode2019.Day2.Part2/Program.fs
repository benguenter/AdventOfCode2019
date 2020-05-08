open IntcodeComputer
open Microsoft.FSharp.Core
open System
open System.IO

type Operation = Add | Multiply | End

[<EntryPoint>]
let main argv =
    let expected = 19690720;
    let opcodes = File.ReadAllText("input.txt").Split [|','|] |> Seq.map Int32.Parse
    let beginning = Seq.take 1 opcodes
    let last = Seq.skip 3 opcodes

    let iterateNounAndVerb noun verb =
        match (noun, verb) with
        | (99, 99) -> failwith "No combination found"
        | (_, 99) -> (noun+1, 0)
        | (_, _) -> (noun, verb+1)

    let computeNounAndVerb noun verb =
        let intcodes = Seq.concat [|beginning; Seq.ofList [noun;verb]; last|]
        IntcodeComputer.Compute intcodes

    let iterateVerbWithNoun (noun, verb) =
        let result = computeNounAndVerb noun verb
        if (result = expected) then
            None
        else
            let nextNounAndVerb = (iterateNounAndVerb noun verb)
            Some (nextNounAndVerb, nextNounAndVerb)

    let result = Seq.unfold iterateVerbWithNoun (0, 0)
    printf "%A" (Seq.last result)

    0 // return an integer exit code
