using SvgIconGenerator;
using System.Text;

namespace MSvg.All;

public record IconFrom(string library, IconDto Icon);
public static class MyExtensions
{
    extension(string value)
    {
        public string NormalizeMe()
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return string.Empty;
            }

            var builder = new StringBuilder(value.Length);

            foreach (var character in value)
            {
                if (char.IsLetterOrDigit(character))
                {
                    builder.Append(char.ToLowerInvariant(character));
                }
            }

            return builder.ToString();

        }
    }


}