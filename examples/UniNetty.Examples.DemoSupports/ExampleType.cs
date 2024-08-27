using UniNetty.Common.Collections.Immutable;

namespace UniNetty.Examples.DemoSupports
{
    public class ExampleType
    {
        public static readonly ExampleType Discard = new ExampleType(nameof(Discard), 7000);
        public static readonly ExampleType Echo = new ExampleType(nameof(Echo), 7010);
        public static readonly ExampleType Factorial = new ExampleType(nameof(Factorial), 7020);
        public static readonly ExampleType HttpServer = new ExampleType(nameof(HttpServer), 7030);
        public static readonly ExampleType QuoteOfTheMoment = new ExampleType(nameof(QuoteOfTheMoment), 7040);
        public static readonly ExampleType SecureChat = new ExampleType(nameof(SecureChat), 7050);
        public static readonly ExampleType Telnet = new ExampleType(nameof(Telnet), 7060);
        public static readonly ExampleType WebSocket = new ExampleType(nameof(WebSocket), 7070);

        public static readonly UniImmutableArray<ExampleType> Values = UniImmutableArrays.CreateRange(
            Discard,
            Echo,
            Factorial,
            HttpServer,
            QuoteOfTheMoment,
            SecureChat,
            Telnet,
            WebSocket
        );

        public readonly string Name;
        public readonly int Port;

        private ExampleType(string name, int port)
        {
            Name = name;
            Port = port;
        }
    }
}