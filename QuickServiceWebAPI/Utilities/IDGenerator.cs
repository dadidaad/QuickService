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

        public static string GeneratePermissionId(int permissionId)
        {
            string formattedId = $"PERM{permissionId.ToString("D6")}";
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
        public static string GenerateServiceItemId(int serviceItemId)
        {
            string formattedId = $"SEIT{serviceItemId.ToString("D6")}";
            return formattedId;
        }
        public static string GenerateServiceDeskHourd(int serviceDeskHourId)
        {
            string formattedId = $"SEDE{serviceDeskHourId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateServiceTypeId(int serviceTypeId)
        {
            string formattedId = $"SETY{serviceTypeId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateBusinessHourId(int businessHourId)
        {
            string formattedId = $"BUSI{businessHourId.ToString("D6")}";
            return formattedId;
        }
        public static string GenerateAttachmentId(int attachmentId)
        {
            string formattedId = $"ATTA{attachmentId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateGroupId(int groupId)
        {
            string formattedId = $"GROU{groupId.ToString("D6")}";
            return formattedId;
        }
        public static string GenerateSlaId(int slaId)
        {
            string formattedId = $"SLA{slaId.ToString("D6")}";
            return formattedId;
        }
        public static string GenerateSlametricId(int slametricId)
        {
            string formattedId = $"SLAM{slametricId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateWorkflowId(int workflowId)
        {
            string formattedId = $"WORK{workflowId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateWorkflowStepId(int workflowStepId)
        {
            string formattedId = $"WOST{workflowStepId.ToString("D6")}";
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
