// Learn more about F# at http://fsharp.org

open System


let lines = [|
    "nop +283";
    "acc +26";
    "acc +37";
    "acc +6";
    "jmp +109";
    "acc +10";
|]

let parseNumber (line:string) = 
    line.[line.IndexOf(" ") + 1..] |> int

let rec analyzeLine (index:int) (linesRan:int[]) (accumulator:int) (modifiedLines:string[]) : int  =
    if index >= modifiedLines.Length then
        // success
        accumulator
    else
        let line = modifiedLines.[index]
        let number = parseNumber(line)
        let existingIndex = linesRan |> Array.filter(fun (l) -> l=index)
        if existingIndex.Length > 0 then
            // weve ran this line before. Exit.
            0
        else
            let newLinesRan = Array.append linesRan [|index|]
            if line.IndexOf("acc") >= 0 then 
                let newIndex = index+1
                let newAccumulator = accumulator+number
                analyzeLine newIndex newLinesRan newAccumulator modifiedLines

            else if line.IndexOf("jmp") >= 0 then
                let newIndex = index + number
                analyzeLine newIndex newLinesRan accumulator modifiedLines

            else if line.IndexOf("nop") >= 0 then
                let newIndex = index + 1
                analyzeLine newIndex newLinesRan accumulator modifiedLines
            else
                // How'd I get here?
                0;

let modifyLineAndRun (line:string) (index:int) (wordToReplace:string) (newWord:string) =
    if (line.IndexOf(wordToReplace) >= 0) then
        let modifiedLine = newWord + line.Substring(3)
        let modifiedFirstHalf = Array.append lines.[0..index-1] [|modifiedLine|]
        let modifiedArray = Array.append modifiedFirstHalf lines.[index+1..lines.Length-1]
        let result = analyzeLine 0 [||] 0 modifiedArray
        if result > 0 then printfn "Result: %i" result

[<EntryPoint>]
let main argv =
    for index = 0 to lines.Length - 1 do
        let line = lines.[index]
        modifyLineAndRun line index "jmp" "nop"
        modifyLineAndRun line index "nop" "jmp"
    0 // return an integer exit code
