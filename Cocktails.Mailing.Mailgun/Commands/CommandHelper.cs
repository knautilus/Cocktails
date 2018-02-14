using System;
using System.Text.RegularExpressions;

namespace Cocktails.Mailing.Mailgun.Commands
{
    internal static class CommandHelper
    {
        public static void CheckNotNullOrEmpty(string value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentException("Value shouldn't be null or empty", paramName);
            }
        }

        public static void CheckLessThan(string value, int maxLength, string paramName)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength)
            {
                throw new ArgumentException("Length of value shouldn't be more than " + maxLength, paramName);
            }
        }

        public static void CheckPureAscii(string value, string paramName)
        {
            if (!Regex.IsMatch(value, "^[a-z0-9_]+$"))
            {
                throw new ArgumentException("Value should be pure ASCII string", paramName);
            }
        }
    }
}
