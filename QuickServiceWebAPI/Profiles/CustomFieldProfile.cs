using AutoMapper;
using QuickServiceWebAPI.DTOs.CustomField;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class CustomFieldProfile : Profile
    {
        public CustomFieldProfile()
        {
            CreateMap<CustomField, CustomFieldDTO>();
            CreateMap<CreateUpdateCustomFieldDTO, CustomField>().IgnoreAllNonExisting();
        }

    }
}
