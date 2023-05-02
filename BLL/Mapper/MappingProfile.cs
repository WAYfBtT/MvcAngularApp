using AutoMapper;
using BLL.Models;
using DAL.Entities;
using System.Linq;

namespace BLL.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Url, UrlModel>()
                .ReverseMap();
            CreateMap<User, UserModel>()
                .ForMember(
                dest => dest.UrlIds,
                opt => opt.MapFrom(src => src.Urls.Select(x => x.Id))
                );
            CreateMap<UserModel, User>()
                .ForMember(
                dest => dest.Urls,
                opt => opt.MapFrom(src => src.UrlIds.Select(x => new Url { Id = x }))
                );
            CreateMap<SignInModel, UserModel>();
            CreateMap<SignUpModel, UserModel>();
        }
    }
}
