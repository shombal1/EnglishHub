using AutoMapper;
using EnglishHub.Domain.Models;
using EnglishHub.Domain.UseCases.SignInUseCase;
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
    }   
}