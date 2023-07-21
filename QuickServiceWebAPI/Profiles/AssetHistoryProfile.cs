using AutoMapper;
using QuickServiceWebAPI.DTOs.AssetHistory;
using QuickServiceWebAPI.DTOs.Group;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class AssetHistoryProfile : Profile
    {
        public AssetHistoryProfile()
        {
            CreateMap<AssetHistory, AssetHistoryDTO>()
                .ForMember(dest => dest.AssetEntity,
                opt => opt.MapFrom(src => src.Asset));
            CreateMap<CreateUpdateAssetHistoryDTO, AssetHistory>();
        }
    }
}
