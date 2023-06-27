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

        public static string GenerateServiceId(int serviceId)
        {
            string formattedId = $"SERV{serviceId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateServiceCategoryId(int serviceCategoryId)
        {
            string formattedId = $"SECA{serviceCategoryId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateServiceTypeId(int serviceTypeId)
        {
            string formattedId = $"SETY{serviceTypeId.ToString("D6")}";
            return formattedId;
        }

        public static int ExtractNumberFromId(string id)
        {
            string resultNumber = Regex.Match(id, REGEXNUMBER).Value;
            return Int32.Parse(resultNumber);
        }

        public static string GenerateRoleId(int roleId)
        {
            string formattedId = $"ROLE{roleId.ToString("D6")}";
            return formattedId;
        }

    }
}
