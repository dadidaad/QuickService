using AutoMapper;
using QuickServiceWebAPI.DTOs.AssetAssignment;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class AssetAssignmentProfile : Profile
    {
        public AssetAssignmentProfile()
        {
            CreateMap<AssetAssignment, AssetAssignmentDTO>()
                .ForMember(dest => dest.AssetEntity,
                opt => opt.MapFrom(src => src.Asset))
                .ForMember(dest => dest.AssignedToUserEntity,
                opt => opt.MapFrom(src => src.AssignedToNavigation));
            CreateMap<CreateUpdateAssetAssignmentDTO, AssetAssignment>();
        }
    }
}
