using AutoMapper;
using JobCandidateHub.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobCandidateHub.Application.Mapper
{
    [ExcludeFromCodeCoverage]
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<ECandidate, ECandidateHistory>()
            .ForMember(dest => dest.CandidateId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
        }
    }
}
