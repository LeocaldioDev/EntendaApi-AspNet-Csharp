using AutoMapper;
using PrimeiraApi.Domain.DTOs;
using PrimeiraApi.Domain.Model.UserAggregate;

namespace PrimeiraApi.Application.Mapping
{
    public class DomainToDTOsMapping : Profile
    {
        public DomainToDTOsMapping() 
        {
            CreateMap<User, UserDTO>()
            .ForMember(dest => dest.NomeUser, m => m.MapFrom(orig => orig.nome)); // essa linha só é necessaria porque os campos nomes são diferentes

        }
    }
}
