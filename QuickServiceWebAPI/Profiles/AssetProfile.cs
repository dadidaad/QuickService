using AutoMapper;
using QuickServiceWebAPI.DTOs.Asset;
using QuickServiceWebAPI.Models;

namespace QuickServiceWebAPI.Profiles
{
    public class AssetProfile : Profile
    {
        public AssetProfile()
        {
            CreateMap<Asset, AssetDTO>();
            CreateMap<CreateUpdateAssetDTO, Asset>();
        }
    }
}
