namespace Signis.CLI

open Signis.Core

type REPLActionType =
    | Unknown of string
    | Empty
    | Exit of string
    | Echo of string

module Constants =
    let exitMessage = "Come back soon T_T"
    let unknownMessage theUnknown = $"HUH: what is `{theUnknown}`?.."

module Parse =
    let command (userInput: string) =
        match Util.firstWordAndRest userInput with
            | "echo", rest -> Echo rest
            | "", _ -> Empty
            | "exit", _ -> Exit Constants.exitMessage
            | cmd, _ -> Unknown cmd