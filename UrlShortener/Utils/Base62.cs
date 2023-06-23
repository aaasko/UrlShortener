using System.Text;

namespace UrlShortener.Utils
{
    public static class Base62
    {
        public static string Encode(long id)
        {
            if (id == 0)
            {
                return "0";
            }

            StringBuilder sb = new();

            for (; id != 0; id /= 62)
            {
                sb.Append(EncodeChar(id % 62));
            }

            return sb.ToString();
        }

        private static char EncodeChar(long c)
        {
            if (c < 10)
            {
                return (char)(c + '0');
            }

            if (c < 36)
            {
                return (char)(c - 10 + 'A');
            }

            return (char)(c - 36 + 'a');
        }
    }
}
