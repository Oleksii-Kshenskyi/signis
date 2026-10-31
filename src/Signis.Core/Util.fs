namespace Signis.Core

open FSharpPlus

module Util =
    let firstWord str =
        String.takeWhile (fun c -> c <> ' ') str
        |> String.trimWhiteSpaces
    
    let dropFirstWord str =
        String.skipWhile (fun c -> c <> ' ') str
        |> String.trimWhiteSpaces