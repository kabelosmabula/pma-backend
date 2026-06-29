using System.Text.RegularExpressions;

namespace Interfaces
{
    public class SlugifyParameterTransformer : IOutboundParameterTransformer
    {
        public string? TransformOutbound(object? value)
        {
            if (value == null) return null;

            var stringValue = value.ToString();
            if (string.IsNullOrWhiteSpace(stringValue)) return null;

            return Regex.Replace(stringValue, "([a-z])([A-Z])", "$1-$2")
                        .ToLowerInvariant();
        }
    }
}
