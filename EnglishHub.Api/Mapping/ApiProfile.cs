using AutoMapper;
using EnglishHub.Domain.Models;

namespace EnglishHub.Api.Mapping;

public class ApiProfile : Profile
{
    public ApiProfile()
    {
        CreateMap<Forum, Models.Forum>()
            .ForMember(f=>f.Title,s=>s.MapFrom(d=>d.Title))
            .ForMember(f=>f.Id,s=>s.MapFrom(d=>d.Id));
        CreateMap<Topic, Models.Topic>();
    }
}