namespace Signis.CLI

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
    let actOnCommand (cmd: REPLActionType) =
        match cmd with
        | Echo what -> $"Echoed: `{what}`"
        | Unknown what -> Constants.unknownMessage what
        | Empty -> ""
        | Exit msg ->
            printfn "%s" msg
            Environment.Exit 0
            ""

    let rec main () = 
        printf "<SIGNIS>> "
        let userInput = 
            Console.ReadLine ()
            |> Option.ofObj
            |> Option.defaultValue "<NULL>"
            |> String.trimWhiteSpaces
        Parse.command userInput
        |> actOnCommand
        |> printfn "%s"

        main ()

    main ()