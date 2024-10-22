using AutoMapper;
using System.Diagnostics.CodeAnalysis;

namespace JobCandidateHub.Infrastructure.Mapper
{
    [ExcludeFromCodeCoverage]
    public static class MapperHelper
    {
        private static IMapper _mapper;
        public static bool _isUnitTest = false;

        public static IMapper Mapper => _mapper;

        public static void Configure(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}
