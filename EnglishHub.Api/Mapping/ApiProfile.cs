using AutoMapper;
using EnglishHub.Domain.Models;

namespace EnglishHub.Mapping;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<Forum, Models.Forum>();
        CreateMap<Topic, Models.Topic>();
    }
}