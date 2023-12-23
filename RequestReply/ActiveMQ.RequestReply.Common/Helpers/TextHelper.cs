namespace ActiveMQ.RequestReply.Common.Helpers;

public static class TextHelper
{
    public const int DefaultTextMessageLength = 10;

    private static readonly Random RandomObject = new Random();

    public static string CreateRandomText(int length)
    {
        var symbols = Enumerable.Range(default, length)
            .Select(i => RandomObject.Next('a', 'z'))
            .Select(value => (char)value)
            .ToArray();

        return new string(symbols);
    }

    public static string ReverseText(string sourceText)
    {
        return new string(sourceText.Reverse().ToArray());
    }
}
