open Microsoft.FSharp.Core
open System
open System.Collections.Generic
open System.IO

type Operation = Add | Multiply | End

[<EntryPoint>]
let main argv = 
    let opcodes = List<int> (File.ReadAllText("input.txt").Split [|','|] |> Seq.map Int32.Parse)
    let operate currentOpcodeGroupIndex operation =
        let destinationIndex = opcodes.[currentOpcodeGroupIndex+3]
        let x = opcodes.[opcodes.[currentOpcodeGroupIndex+1]]
        let y = opcodes.[opcodes.[currentOpcodeGroupIndex+2]]

        match operation with
        | Add -> Some (opcodes.[destinationIndex] <- x + y)
        | Multiply -> Some (opcodes.[destinationIndex] <- x * y)
        | End -> None

    let iterateAndOperate currentOpcodeGroupIndex =
        let operation =
            match opcodes.[currentOpcodeGroupIndex] with
            | 1 -> Add
            | 2 -> Multiply
            | 99 -> End
            | _ -> failwith (sprintf "Invalid Opcode: '%i'" opcodes.[currentOpcodeGroupIndex])
        let result = operate (currentOpcodeGroupIndex) operation
        match result with
        | Some _ -> Some (opcodes.[0], currentOpcodeGroupIndex + 4)
        | None -> None

    // Using unfold so that we can end on demand (return of None)
    Seq.unfold iterateAndOperate 0 |> Seq.toList |> ignore
    printfn "%i" opcodes.[0]
    0 // return an integer exit code
