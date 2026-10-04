using System.Diagnostics;
using Signis.Engine;

// TODO[[1]]: Get the standard loop set up (exit, echo, unknown, empty commands).
while(true) {
    Console.Write("[SGN] ");
    var user_input = Console.ReadLine();
    var result = Executor.ResolveCommand(user_input);
    Console.WriteLine(PrintResult(result));
}

static string PerformExit() {
    Console.WriteLine("Thanks for playing! Bye! ^_^");
    Environment.Exit(0);
    return "";
}

static string PrintResult(REPLResult res) => res switch {
    REPLResult.Exit => PerformExit(),
    REPLResult.Empty => "",
    REPLResult.Unknown(var verb) => $"[???] No clue what {verb} is!",
    REPLResult.WrongWordCount((var upper, var lower), var actual) => $"[T_T] Expected {lower} to {upper} words in this command, got {actual}!",
    REPLResult.ActIngame(var action) => string.Join('\n', Executor.Act(action)),
    _ => throw new UnreachableException($"Main.PrintResult(): unexpected REPL result {res.GetType().Name}")
};
