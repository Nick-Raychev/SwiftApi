using System;

namespace SwiftMT799Api
{
    public class SwiftMessageParser
    {
        public SwiftMessage Parse(string rawMessage)
        {
            var swiftMessage = new SwiftMessage();

            var lines = rawMessage.Split('\n');
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length >= 2)
                {
                    var field = parts[0].Trim();
                    var value = parts[1].Trim();

                    // Add checks for specific fields and value extraction
                    switch (field)
                    {
                        case "20":
                            swiftMessage.Field20 = value;
                            break;
                        case "21":
                            swiftMessage.Field21 = value;
                            break;
                        case "79":
                            swiftMessage.Field79 = value;
                            break;
                        // Add other fields as needed
                    }
                }
            }

            return swiftMessage;
        }
    }
}