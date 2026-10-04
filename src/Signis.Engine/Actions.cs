using System.Diagnostics;

namespace Signis.Engine;

public abstract record REPLResult {
    private REPLResult() {}

    public sealed record ActIngame(GameAction Action): REPLResult;
    public sealed record Unknown(string UnknownCommand): REPLResult;
    public sealed record WrongWordCount((ushort, ushort) ExpectedRange, ushort Actual): REPLResult;
    public sealed record Empty: REPLResult;
    public sealed record Exit: REPLResult;
}

public abstract record GameAction {
    private GameAction() {}

    public sealed record Echo(string What): GameAction;
    public sealed record Time(): GameAction;
}

public static class Executor {
    public static List<string> Act(GameAction action) => action switch {
        GameAction.Echo(string what) => [$"Echoed: `{what}`"],
        GameAction.Time => [$"Time now: {DateTime.Now}."],

        _ => throw new UnreachableException($"Executor.Act(): unhandled GameAction type `{action.GetType().Name}`.")
    };

    public static REPLResult ResolveCommand(string? user_input) {
        user_input = string.IsNullOrEmpty(user_input) ? "" : user_input;
        var grouped_by_first = ParseHelper.GroupWords(user_input, 1);
        if(grouped_by_first.Item1.Length == 0 || string.IsNullOrEmpty(grouped_by_first.Item1[0])) {
            return new REPLResult.Empty();
        }
        var first_word = grouped_by_first.Item1[0];
        var spec = ResolverMap.GetValueOrDefault(first_word);
        if(spec is null) {
            return UnknownMapper(first_word);
        }
        if(!IsWordCountInRange(user_input, spec.WordCountRange)) {
            return new REPLResult.WrongWordCount(spec.WordCountRange, (ushort)(grouped_by_first.Item1.Length + grouped_by_first.Item2.Length)) {};
        }

        return spec.ResultMapper(user_input);
    }

    public sealed record VerbSpec(Func<string, REPLResult> ResultMapper, (ushort, ushort) WordCountRange);

    private static REPLResult UnknownMapper(string UnknownVerb) {
        return new REPLResult.Unknown(UnknownVerb) {};
    }

    private static bool IsWordCountInRange(string user_input, (ushort, ushort) range) {
        if(range.Item1 == 0 && range.Item2 == 0) return true;

        var word_count = ParseHelper.Words(user_input).Length;
        var lower_in_range = range.Item1 == 0 || word_count >= range.Item1;
        var upper_in_range = range.Item2 == 0 || word_count <= range.Item2;

        return lower_in_range && upper_in_range;
    }

    public static readonly Dictionary<string, VerbSpec> ResolverMap = new() {
        { "exit", new((_) => new REPLResult.Exit {}, (1, 1)) { } },
        { "time", new((_) => new REPLResult.ActIngame(new GameAction.Time {}), (1, 1)) {}},
        { "echo", new((echoed) => new REPLResult.ActIngame(new GameAction.Echo(string.Join(" ", ParseHelper.GroupWords(echoed, 1).Item2)) {}) {}, (1,0)) {}},
    };
}

