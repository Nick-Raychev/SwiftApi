using System.Text;
using Microsoft.Extensions.Logging;
using System;

namespace SwiftMT799Api
{
    public class SwiftMessage
    {
        public string? Field20 { get; set; }
        public string? Field21 { get; set; }
        public string? Field79 { get; set; }

        // Other properties...

        public SwiftMessage()
        {
            Field20 = string.Empty;
            Field21 = string.Empty;
            Field79 = string.Empty;

            // Initialize other properties...
        }

        public override string ToString()
        {
            return $"Field20: {Field20}, Field21: {Field21}, Field79: {Field79}";
        }
    }
}