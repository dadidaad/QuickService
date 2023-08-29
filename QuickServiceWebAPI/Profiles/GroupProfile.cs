using AutoMapper;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class GroupProfile : Profile
    {
        public GroupProfile()
        {
            CreateMap<Group, GroupDTO>()
                .ForMember(dest => dest.UserEntity,
                opt => opt.MapFrom(src => src.GroupLeaderNavigation));
            CreateMap<CreateUpdateGroupDTO, Group>();
        }
    }
}
