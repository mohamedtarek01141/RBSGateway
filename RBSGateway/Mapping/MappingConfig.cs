using AutoMapper;
using RBSGateway.DTO.Resource;
using RBSGateway.Entities;

namespace RBSGateway.Mapping
{
    public class MappingConfig :Profile
    {
        public MappingConfig()
        {

            CreateMap<GetResourceDto, Resource>()
            .ForPath(dest => dest.ResourceName.Name, opt => opt.MapFrom(src => src.ResourceName))
            .ForPath(dest => dest.ResourceName.Language, opt => opt.MapFrom(src => src.Language));
            CreateMap<Resource, GetResourceDto>()
                .ForPath(dest => dest.ResourceName, opt => opt.MapFrom(src => src.ResourceName.Name))
            .ForPath(dest => dest.Language, opt => opt.MapFrom(src => src.ResourceName.Language));
            CreateMap<CreateResourceDto, Resource>()
                .ForMember(dest => dest.ResourceNameId, opt => opt.Ignore()) 
                .ForMember(dest => dest.ResourceName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore()) 
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore()); 
            CreateMap<UpdateResourceDto, Resource>()
             .ForMember(dest => dest.ResourceNameId, opt => opt.Ignore()) 
                .ForMember(dest => dest.ResourceName, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedBy, opt => opt.Ignore()) 
                .ForMember(dest => dest.CreatedDate, opt => opt.Ignore()) 
                .ForMember(dest => dest.LastUpdatedBy, opt => opt.Ignore()) 
                .ForMember(dest => dest.LastUpdatedDate, opt => opt.Ignore()); 
        }

    }
}
