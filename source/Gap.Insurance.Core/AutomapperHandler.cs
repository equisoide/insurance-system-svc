using System.Diagnostics.CodeAnalysis;
using System.Linq;
using AutoMapper;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    [SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Config is injected from the startup pipeline, never is null.")]
    public static class AutomapperHandler
    {
        public static void MapPayloadsToEntities(this IMapperConfigurationExpression config)
        {
            config.CreateMap<CreatePolicyPayload, Policy>();
        }

        public static void MapEntitiesToDtos(this IMapperConfigurationExpression config)
        {
            config.CreateMap<Risk, RiskDto>();
            config.CreateMap<Coverage, CoverageDto>();

            config.CreateMap<Policy, PolicyDto>()
                .ForMember(
                    des => des.RiskDescripition,
                    opt => opt.MapFrom(src => src.Risk.Description))
                .ForMember(
                    des => des.Coverages,
                    opt => opt.MapFrom(src => src.PolicyCoverage.Select(pc => new PolicyCoverageDto {
                        PolicyCoverageId = pc.PolicyCoverageId,
                        CoverageDescription = pc.Coverage.Description,
                        Percentage = pc.Percentage
                    })));
        }
    }
}
