using UniNetty.Common.Collections.Immutable;

namespace UniNetty.Examples.DemoSupports
{
    public class DemoType
    {
        public static readonly DemoType Discard = new DemoType(nameof(Discard), 7000);
        public static readonly DemoType Echo = new DemoType(nameof(Echo), 7010);
        public static readonly DemoType Factorial = new DemoType(nameof(Factorial), 7020);
        public static readonly DemoType HttpServer = new DemoType(nameof(HttpServer), 7030);
        public static readonly DemoType QuoteOfTheMoment = new DemoType(nameof(QuoteOfTheMoment), 7040);
        public static readonly DemoType SecureChat = new DemoType(nameof(SecureChat), 7050);
        public static readonly DemoType Telnet = new DemoType(nameof(Telnet), 7060);
        public static readonly DemoType WebSocket = new DemoType(nameof(WebSocket), 7070);

        public static readonly UniImmutableArray<DemoType> Values = UniImmutableArrays.CreateRange(
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

        private DemoType(string name, int port)
        {
            Name = name;
            Port = port;
        }
    }
}