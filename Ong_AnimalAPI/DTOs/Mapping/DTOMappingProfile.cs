using AutoMapper;
using Ong_AnimalAPI.Models;

namespace Ong_AnimalAPI.DTOs.Mapping
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<Animal, AnimalDTO>().ReverseMap();        
        }
    }
}
