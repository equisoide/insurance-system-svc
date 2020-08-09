using System.Linq;
using AutoMapper;
using Gap.Insurance.EntityFramework;
using Gap.Insurance.Model;

namespace Gap.Insurance.Core
{
    public static class AutomapperHandler
    {
        public static void MapPayloadsToEntities(this IMapperConfigurationExpression config)
        {
            config.CreateMap<CreatePolicyPayload, Policy>();
            config.CreateMap<UpdatePolicyPayload, Policy>();
            config.CreateMap<CreatePolicyCoveragePayload, PolicyCoverage>();

            config.CreateMap<CreateClientPolicyPayload, ClientPolicy>()
                .ForMember(
                    des => des.PolicyStatusId,
                    opt => opt.MapFrom(src => Constants.ActivePolicyStatusId));
        }

        public static void MapEntitiesToDtos(this IMapperConfigurationExpression config)
        {
            config.CreateMap<Risk, RiskDto>();
            config.CreateMap<Coverage, CoverageDto>();
            config.CreateMap<Client, ClientDto>();

            config.CreateMap<Policy, PolicyDto>()
                .ForMember(
                    des => des.RiskDescripition,
                    opt => opt.MapFrom(src => src.Risk.Description))
                .ForMember(
                    des => des.MaxCoverage,
                    opt => opt.MapFrom(src => src.Risk.MaxCoverage))
                .ForMember(
                    des => des.Coverages,
                    opt => opt.MapFrom(src => src.PolicyCoverage.Select(pc => new PolicyCoverageDto {
                        PolicyCoverageId = pc.PolicyCoverageId,
                        CoverageDescription = pc.Coverage.Description,
                        Percentage = pc.Percentage
                    })));

            config.CreateMap<PolicyCoverage, PolicyCoverageDto>()
                .ForMember(
                    des => des.CoverageDescription,
                    opt => opt.MapFrom(src => src.Coverage.Description));

            config.CreateMap<ClientPolicy, ClientPolicyDto>()
                .ForMember(
                    des => des.PolicyName,
                    opt => opt.MapFrom(src => src.Policy.Name))
                .ForMember(
                    des => des.PolicyStatusDescription,
                    opt => opt.MapFrom(src => src.PolicyStatus.Description));
        }
    }
}
