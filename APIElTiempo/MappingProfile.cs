using APIElTiempo.Models;
using AutoMapper;
using static APIElTiempo.Models.Users;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<Role, RoleDto>().ReverseMap();
        CreateMap<Article, ArticleDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author.Username))
            .ReverseMap();
        CreateMap<ArticleImage, ArticleImageDto>().ReverseMap();
    }

}