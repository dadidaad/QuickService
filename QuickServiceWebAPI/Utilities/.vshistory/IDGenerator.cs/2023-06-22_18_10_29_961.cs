using System.Text.RegularExpressions;

namespace QuickServiceWebAPI.Utilities
{
    public static class IDGenerator
    {
        private static string REGEXNUMBER = @"\d+";
        
        public static string GenerateUserId(int userId)
        {
            string formattedId = $"USER{userId.ToString("D6")}";
            return formattedId;
        }

        public static int ExtractNumberFromId(string id)
        {
            string resultNumber = Regex.Match(id, REGEXNUMBER).Value;
            return Int32.Parse(resultNumber);
        }
    }
}
