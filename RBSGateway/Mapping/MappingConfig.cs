using AutoMapper;
using RBSGateway.DTO.Resource;
using RBSGateway.Entities;

namespace RBSGateway.Mapping
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {
            CreateMap<GetResourceDto, Resource>();
            CreateMap<Resource, GetResourceDto>();
            CreateMap<CreateResourceDto, Resource>()
                .ForMember(dest => dest.ResourceNameId, opt => opt.Ignore()) // Ignored because it's set manually
                .ForMember(dest => dest.ResourceName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore()) // Not in Create DTO
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore()); // Set in service
            CreateMap<UpdateResourceDto, Resource>()
                .ForMember(dest => dest.ResourceNameId, opt => opt.Ignore()) // Ignored because it's set manually
                .ForMember(dest => dest.ResourceName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) // Set in service
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore()) // Not in Create DTO
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore()); // Set in service
        }

    }
}
