namespace Signis.Engine;

static class ParseHelper {
    public static string[] Words(string str) {
        return str.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }

    /// <summary>
    /// Splits a string into two groups of words.
    /// </summary>
    /// <param name="str">The string to split.</param>
    /// <param name="first_group_word_count">Number of words in the first group. Thus the second group has number_of_words_in_str - first_group_word_count words.</param>
    /// <returns></returns>
    public static (string[], string[]) GroupWords(string str, ushort first_group_word_count) {
        var words = Words(str);
        if(words is null or []) {
            return ([], []);
        } else return (words[0..first_group_word_count], words[first_group_word_count..]);
    }
}