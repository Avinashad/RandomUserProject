using AutoMapper;
using RandomUserCore.ExternalApi;
using RandomUserCore.Models;
using RandomUserCore.Models.IntegrationModels;

namespace RandomUserCore.Mapping
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<User, UserEntity>();
            CreateMap<UserEntity, User>();

            CreateMap<ImageDetail, ImageDetailEntity>();
            CreateMap<ImageDetailEntity, ImageDetail>();

            CreateMap<UserModel, UserEntity>()
            .ForMember(u => u.FirstName, m => m.MapFrom(src => src.Name.First))
            .ForMember(u => u.LastName, m => m.MapFrom(src => src.Name.Last))
            .ForMember(u => u.Title, m => m.MapFrom(src => src.Name.Title))
            .ForMember(u => u.PhoneNumber, m => m.MapFrom(src => src.Phone))
            .ForMember(u => u.Email, m => m.MapFrom(src => src.Email))
            .ForMember(u => u.DateOfBirth, m => m.MapFrom(src => src.Dob.Date))
            .ForPath(u => u.ImageDetail.Original, m => m.MapFrom(src => src.Picture.Large))
            .ForPath(u => u.ImageDetail.Thumbnail, m => m.MapFrom(src => src.Picture.thumbnail));
        }
    }
}