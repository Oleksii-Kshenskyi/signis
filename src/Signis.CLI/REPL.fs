open System
open FSharpPlus

open Signis.Core

/// This is the CLI module. Main purpose is: REPL, CLI interface, IO.
/// IO, imperative code and any other impurities are allowed here.
/// Working with C# and thirdparty code should be isolated to the
/// Adapters project and only invoked from here to glue everything together
/// for IO/UX purposes.
/// Uses:
/// - Core for domain types and logic;
/// - Adapters for third-party and C# code (dependencies);
/// Only composes the dependencies and applies IO.
/// Does not contain logic or computations, is an impure 
/// constructor/consumer module.

module REPL =
    // let parseCommand (userInput: string) =
    //    match userInput.StartsWith ""

    let rec main () = 
        printf "<SIGNIST>> "
        let userInput = 
            Console.ReadLine ()
            |> Option.ofObj
            |> Option.defaultValue "<NULL>"
            |> String.trimWhiteSpaces
        printfn "Echoed: %s" userInput
        printfn "Skipped first word: %s" (Util.dropFirstWord userInput)
        printfn "First word was: %s" (Util.firstWord userInput)
        main ()
    
    main ()