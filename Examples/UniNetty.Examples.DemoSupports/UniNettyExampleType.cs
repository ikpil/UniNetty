using UniNetty.Common.Collections.Immutable;

namespace UniNetty.Examples.DemoSupports
{
    /*
     * {
  "ssl": "false",
  "host": "127.0.0.1",
  "port": "8080",
  "path": "/websocket",
  "size": "256",
  "count": "100",
  "Logging": {
    "LogLevel": {
      "Default": "Trace"
    }
  }
}
     */
    public class UniNettyExampleType
    {
        public static readonly UniNettyExampleType None = new UniNettyExampleType(0, nameof(None));
        public static readonly UniNettyExampleType Discard = new UniNettyExampleType(1, nameof(Discard));
        public static readonly UniNettyExampleType Echo = new UniNettyExampleType(2, nameof(Echo));
        public static readonly UniNettyExampleType Factorial = new UniNettyExampleType(3, nameof(Factorial));
        public static readonly UniNettyExampleType HttpServer = new UniNettyExampleType(4, nameof(HttpServer));
        public static readonly UniNettyExampleType QuoteOfTheMoment = new UniNettyExampleType(5, nameof(QuoteOfTheMoment));
        public static readonly UniNettyExampleType SecureChat = new UniNettyExampleType(6, nameof(SecureChat));
        public static readonly UniNettyExampleType Telnet = new UniNettyExampleType(7, nameof(Telnet));
        public static readonly UniNettyExampleType WebSocket = new UniNettyExampleType(8, nameof(WebSocket));

        public static readonly UniImmutableArray<UniNettyExampleType> Values = UniImmutableArrays.CreateRange(
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

        private UniNettyExampleType(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}