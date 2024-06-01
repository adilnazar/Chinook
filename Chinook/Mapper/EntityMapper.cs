using AutoMapper;
using Chinook.ClientModels;
using Chinook.Common.Constants;
using Chinook.Models;

namespace Chinook.Mapper
{
    public class EntityMapper : Profile
    {
        public EntityMapper()
        {
            CreateMap<Artist, ArtistModel>()
            .ForMember(dest => dest.AlbumCount, opt => opt.MapFrom(src => src.Albums.Count));
        }
    }
}
