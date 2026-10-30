namespace Signis.Contracts

/// Contracts defines the protocol of communication between Core and the outside world.
/// It is a tiny layer of shared types that depends on nothing.
/// Even Core is allowed to depend on Contracts (because to know the
/// interface of communication with the outside world it naturally needs
/// to know the type signatures of said interface).
/// Rules of getting into Contracts:
/// - You're a type (not a behavior);
/// - You don't depend on anything (no third-party types either);
/// - You are a shared type between Core, Adapters and CLI;
/// A type should be in Core and NOT in Contracts if it is related to the domain
/// logic or doesn't need to get shared between pure and impure code.
/// If a type is used by CLI/IO but NOT Adapters, it can stay in Core because
/// CLI has direct access to Core's domain types.

module Say =
    let hello name =
        printfn "Hello %s" name
