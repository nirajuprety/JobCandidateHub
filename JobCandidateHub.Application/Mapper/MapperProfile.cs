using AutoMapper;
using JobCandidateHub.Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace JobCandidateHub.Application.Mapper
{
    [ExcludeFromCodeCoverage]
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ECandidate, ECandidateHistory>()
            .ForMember(dest => dest.CandidateId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        }
    }
}
