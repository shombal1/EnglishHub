using AutoMapper;
using EnglishHub.Domain.Models;
using EnglishHub.Storage.Models;

namespace EnglishHub.Storage.Mapping;

public class StorageProfile : Profile
{
    public StorageProfile()
    {
        CreateMap<ForumEntity, Forum>();
        CreateMap<TopicEntity, Topic>();
    }   
}