namespace Signis.Core

/// Contains the main logic and domain types of SIGNIS.
/// This is the pure Core module.
/// ONLY pure functions are allowed here.
/// The Core only depends on Contracts, which expose shared types.
/// Rules for getting into the Core:
/// - No IO
/// - No side effects
/// - No mutable state
/// - No exceptions (error handling via monadic types)
/// - No third-party types or code.
/// SIGNIS.Core.Interface module is the public-facing API of the Core assembly.
/// SIGNIS.Core.DomainTypes should be treated as the domain specification of the game.

module Say =
    let hello name =
        printfn "Hello %s" name
