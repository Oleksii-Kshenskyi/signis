using System.Diagnostics;
using Signis.Engine;

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


static string WrongWordCountMessage(ushort lower, ushort upper, ushort actual) {
    if(lower == upper) {
        return $"Expected exactly {lower} words in this command, got {actual}!";
    }
    if(lower == 0 && upper != 0) {
        return $"Expected no more than {upper} words in this command, got {actual}!";
    } else if(lower != 0 && upper == 0) {
        return $"Expected at least {lower} words in the command, got {actual}!";
    } else {
        return $"Expected {lower} to {upper} words in this command, got {actual}!";
    }
}

static string PrintResult(REPLResult res) => res switch {
    REPLResult.Exit => PerformExit(),
    REPLResult.Empty => "",
    REPLResult.Unknown(var verb) => $"[???] No clue what {verb} is!",
    REPLResult.WrongWordCount((var lower, var upper), var actual) => WrongWordCountMessage(lower, upper, actual),
    REPLResult.ActIngame(var action) => string.Join('\n', Executor.Act(action)),
    _ => throw new UnreachableException($"Main.PrintResult(): unexpected REPL result {res.GetType().Name}")
};
