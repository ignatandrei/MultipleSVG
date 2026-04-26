using System.Text;

namespace MSvg.All;
public static class MyExtensions
{
    extension(string value)
    {
        public string Normalize()
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