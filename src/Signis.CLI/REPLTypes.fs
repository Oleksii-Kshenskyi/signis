namespace Signis.CLI

type REPLActionType =
    Unknown
    | Empty
    | Exit of string
    | Echo of string