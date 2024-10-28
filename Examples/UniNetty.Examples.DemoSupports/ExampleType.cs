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
    public class ExampleType
    {
        public static readonly ExampleType None = new ExampleType(0, nameof(None));
        public static readonly ExampleType Discard = new ExampleType(1, nameof(Discard));
        public static readonly ExampleType Echo = new ExampleType(2, nameof(Echo));
        public static readonly ExampleType Factorial = new ExampleType(3, nameof(Factorial));
        public static readonly ExampleType HttpServer = new ExampleType(4, nameof(HttpServer));
        public static readonly ExampleType QuoteOfTheMoment = new ExampleType(5, nameof(QuoteOfTheMoment));
        public static readonly ExampleType SecureChat = new ExampleType(6, nameof(SecureChat));
        public static readonly ExampleType Telnet = new ExampleType(7, nameof(Telnet));
        public static readonly ExampleType WebSocket = new ExampleType(8, nameof(WebSocket));

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

        private ExampleType(int index, string name)
        {
            Index = index;
            Name = name;
        }
    }
}