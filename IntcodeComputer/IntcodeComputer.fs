namespace IntcodeComputer

open System.Collections.Generic

type Operation = Add | Multiply | End

type IntcodeComputer() = 
    static member Compute (opcodes: seq<int>) =
        let memory = List<int> opcodes
        let operate currentOpcodeGroupIndex operation =
            let destinationIndex = memory.[currentOpcodeGroupIndex+3]
            let x = memory.[memory.[currentOpcodeGroupIndex+1]]
            let y = memory.[memory.[currentOpcodeGroupIndex+2]]

            match operation with
            | Add -> Some (memory.[destinationIndex] <- x + y)
            | Multiply -> Some (memory.[destinationIndex] <- x * y)
            | End -> None

        let iterateAndOperate currentOpcodeGroupIndex =
            let operation =
                match memory.[currentOpcodeGroupIndex] with
                | 1 -> Add
                | 2 -> Multiply
                | 99 -> End
                | _ -> failwith (sprintf "Invalid Opcode: '%i'" memory.[currentOpcodeGroupIndex])
            let result = operate (currentOpcodeGroupIndex) operation
            match result with
            | Some _ -> Some (memory.[0], currentOpcodeGroupIndex + 4)
            | None -> None

        // Using unfold so that we can end on demand (return of None)
        Seq.unfold iterateAndOperate 0 |> Seq.toList |> ignore
        memory.[0]
