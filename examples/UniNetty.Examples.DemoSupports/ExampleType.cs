using UniNetty.Common.Collections.Immutable;

namespace UniNetty.Examples.DemoSupports
{
    public class ExampleType
    {
        public static readonly ExampleType None = new ExampleType(0, nameof(None), 0);
        public static readonly ExampleType Discard = new ExampleType(1, nameof(Discard), 7000);
        public static readonly ExampleType Echo = new ExampleType(2, nameof(Echo), 7010);
        public static readonly ExampleType Factorial = new ExampleType(3, nameof(Factorial), 7020);
        public static readonly ExampleType HttpServer = new ExampleType(4, nameof(HttpServer), 7030);
        public static readonly ExampleType QuoteOfTheMoment = new ExampleType(5, nameof(QuoteOfTheMoment), 7040);
        public static readonly ExampleType SecureChat = new ExampleType(6, nameof(SecureChat), 7050);
        public static readonly ExampleType Telnet = new ExampleType(7, nameof(Telnet), 7060);
        public static readonly ExampleType WebSocket = new ExampleType(8, nameof(WebSocket), 7070);

        public static readonly UniImmutableArray<ExampleType> Values = UniImmutableArrays.CreateRange(
            None,
            Discard,
            Echo,
            Factorial,
            HttpServer,
            QuoteOfTheMoment,
            SecureChat,
            Telnet,
            WebSocket
        );

        public readonly int Index;
        public readonly string Name;
        public readonly int Port;

        private ExampleType(int index, string name, int port)
        {
            Index = index;
            Name = name;
            Port = port;
        }
    }
}