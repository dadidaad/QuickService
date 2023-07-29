using AutoMapper;
using QuickServiceWebAPI.DTOs.Change;
using QuickServiceWebAPI.Models;
using QuickServiceWebAPI.Utilities;

namespace QuickServiceWebAPI.Profiles
{
    public class ChangeProfile : Profile
    {
        public ChangeProfile()
        {
            CreateMap<CreateChangeDTO, Change>().IgnoreAllNonExisting();
        }
    }
}
