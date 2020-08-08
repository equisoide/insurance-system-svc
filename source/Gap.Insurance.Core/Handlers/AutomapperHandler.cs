using System.Diagnostics.CodeAnalysis;
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
        }

        public static void MapEntitiesToDtos(this IMapperConfigurationExpression config)
        {
            config.CreateMap<Risk, RiskDto>();
            config.CreateMap<Coverage, CoverageDto>();
        }
    }
}
