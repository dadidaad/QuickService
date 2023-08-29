using System.Text.RegularExpressions;

namespace QuickServiceWebAPI.Utilities
{
    public static class IDGenerator
    {
        private static string REGEXNUMBER = @"\d+";

        public static string GenerateUserId(int userId)
        {
            string formattedId = $"USER{userId:D6}";
            return formattedId;
        }

        public static string GeneratePermissionId(int permissionId)
        {
            string formattedId = $"PERM{permissionId:D6}";
            return formattedId;
        }

        public static string GenerateServiceId(int serviceId)
        {
            string formattedId = $"SERV{serviceId:D6}";
            return formattedId;
        }

        public static string GenerateServiceCategoryId(int serviceCategoryId)
        {
            string formattedId = $"SECA{serviceCategoryId:D6}";
            return formattedId;
        }

        public static string GenerateRequestTicketHistoryId(int requestTicketHistoryId)
        {
            string formattedId = $"RETH{requestTicketHistoryId:D6}";
            return formattedId;
        }
        public static string GenerateServiceItemId(int serviceItemId)
        {
            string formattedId = $"SEIT{serviceItemId:D6}";
            return formattedId;
        }
        public static string GenerateServiceDeskHourd(int serviceDeskHourId)
        {
            string formattedId = $"SEDE{serviceDeskHourId:D6}";
            return formattedId;
        }

        public static string GenerateServiceTypeId(int serviceTypeId)
        {
            string formattedId = $"SETY{serviceTypeId:D6}";
            return formattedId;
        }

        public static string GenerateBusinessHourId(int businessHourId)
        {
            string formattedId = $"BUSI{businessHourId:D6}";
            return formattedId;
        }
        public static string GenerateCommentId(int commentId)
        {
            string formattedId = $"COMM{commentId:D6}";
            return formattedId;
        }
        public static string GenerateAttachmentId(int attachmentId)
        {
            string formattedId = $"ATTA{attachmentId:D6}";
            return formattedId;
        }

        public static string GenerateAssetId(int assetId)
        {
            string formattedId = $"ASSE{assetId:D6}";
            return formattedId;
        }

        public static string GenerateAssetHistoryId(int assetHistoryId)
        {
            string formattedId = $"ASHI{assetHistoryId:D6}";
            return formattedId;
        }

        public static string GenerateAssetAssignmentId(int assetAssignmentId)
        {
            string formattedId = $"ASAS{assetAssignmentId:D6}";
            return formattedId;
        }

        public static string GenerateGroupId(int groupId)
        {
            string formattedId = $"GROU{groupId.ToString("D6")}";
            return formattedId;
        }

        public static string GenerateRequestTicketExtId(int requestTicketExtId)
        {
            string formattedId = $"RETE{requestTicketExtId:D6}";
            return formattedId;
        }

        public static string GenerateSlaId(int slaId)
        {
            string formattedId = $"SELA{slaId:D6}";
            return formattedId;
        }
        public static string GenerateSlametricId(int slametricId)
        {
            string formattedId = $"SLAM{slametricId:D6}";
            return formattedId;
        }

        public static string GenerateWorkflowId(int workflowId)
        {
            string formattedId = $"WORK{workflowId:D6}";
            return formattedId;
        }

        public static string GenerateWorkflowTransitionId(int workflowTransitionId)
        {
            string formattedId = $"WKTR{workflowTransitionId:D6}";
            return formattedId;
        }

        public static string GenerateWorkflowAssignmentId(int workflowAssignmentId)
        {
            string formattedId = $"WKAS{workflowAssignmentId:D6}";
            return formattedId;
        }

        public static string GenerateWorkflowTaskId(int workflowStepId)
        {
            string formattedId = $"WOST{workflowStepId:D6}";
            return formattedId;
        }

        public static int ExtractNumberFromId(string id)
        {
            string resultNumber = Regex.Match(id, REGEXNUMBER).Value;
            return Int32.Parse(resultNumber);
        }
        public static string GenerateQueryId(int queryId)
        {
            string formattedId = $"QUER{queryId:D6}";
            return formattedId;
        }
        public static string GenerateRoleId(int roleId)
        {
            string formattedId = $"ROLE{roleId:D6}";
            return formattedId;
        }

        public static string GenerateCustomFieldId(int customFieldId)
        {
            string formattedId = $"CUFD{customFieldId:D6}";
            return formattedId;
        }

        public static string GenerateRequestTicketId(int requestTicketId)
        {
            string formattedId = $"RETK{requestTicketId:D6}";
            return formattedId;
        }

        public static string GenerateChangeId(int changeId)
        {
            string formattedId = $"CHNG{changeId:D6}";
            return formattedId;
        }

        public static string GenerateProblemId(int problemId)
        {
            string formattedId = $"PRBL{problemId:D6}";
            return formattedId;
        }

        public static string GenerateNotificationId(int notificationId)
        {
            string formattedId = $"NOTI{notificationId:D6}";
            return formattedId;
        }
    }
}
