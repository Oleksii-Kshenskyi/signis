namespace Signis.Adapters

/// This is the interface/API module of the Adapters assembly.
/// Main purpose is to isolate/wrap third-party and C# code for
/// easy/functional consumption by CLI.
/// Adapters can:
/// - Depend on third-party and C# code (dependencies);
/// - Depend on Contracts (shared types);
/// - Implement Contracts types for composition with Core inside CLI (CLI is the only composition layer that's allowed to use all three);
/// - Use impure/imperative styles;
/// Any IO done for the sake of CLI/UX should be done in CLI, not here.

module Say =
    let hello name =
        printfn "Hello %s" name
