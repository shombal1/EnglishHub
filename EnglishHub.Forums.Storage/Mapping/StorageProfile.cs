using AutoMapper;
using EnglishHub.Forums.Domain.Authentication;
using EnglishHub.Forums.Domain.Models;
using EnglishHub.Forums.Domain.UseCases.SignIn;
using EnglishHub.Storage.Models;

namespace EnglishHub.Storage.Mapping;

public class StorageProfile : Profile
{
    public StorageProfile()
    {
        CreateMap<ForumEntity, Forum>();
        CreateMap<TopicEntity, Topic>();
        CreateMap<UserEntity, RecognizeUser>()
            .ForMember(d=>d.UserId,s=>s.MapFrom(u=>u.Id));
        CreateMap<SessionEntity, Session>();
    }   
}