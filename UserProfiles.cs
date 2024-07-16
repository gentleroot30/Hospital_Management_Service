/*using AutoMapper;
using HospitalMgmtService.Database;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using static HospitalMgmtService.RequestResponseModel.ResponseModel.GetRoleResponse;
using ServiceStack;

namespace HospitalMgmtService
{
    public class UserProfiles : Profile
    {
        public UserProfiles() {
            CreateMap<Customer, GetCustomerResponse>();
            CreateMap<GetCustomerResponse, Customer>();
            CreateMap<Brand, GetBrandResponse>();
            CreateMap<GetBrandResponse, Brand>();
            CreateMap<






, GetCustomerCategoryResponse>();
            CreateMap<GetCustomerCategoryResponse, CustomerCategory>();
            CreateMap<Product, GetProductResponse>();
         
            CreateMap<Role, RolesDTO>()
                 .ForMember(src => src.roleId, opt => opt.MapFrom(dst => dst.roleId))
                 .ForMember(src => src.FeaturesDTO, opt => opt.MapFrom(dst => dst.RoleFeatures));

             CreateMap<RoleFeature, FeaturesDTO>()
             .ForMember(dest => dest.featureId, opt => opt.MapFrom(src => src.Feature.featureId))
             .ForMember(dest => dest.FeatureName, opt => opt.MapFrom(src => src.Feature.FeatureName))
             .ForMember(dest => dest.view, opt => opt.MapFrom(src => src.view))
             .ForMember(dest => dest.add, opt => opt.MapFrom(src => src.add))
             .ForMember(dest => dest.edit, opt => opt.MapFrom(src => src.edit))
             .ForMember(dest => dest.delete, opt => opt.MapFrom(src => src.delete));

          CreateMap<Role, AddRoleRequestModel>()
                 .ForMember(src => src.roleName, opt => opt.MapFrom(dst => dst.roleName))
                 .ForMember(src => src.description, opt => opt.MapFrom(dst => dst.description))
                 .ForMember(src => src.AddFeatureDTO, opt => opt.MapFrom(dst => dst.RoleFeatures)).ReverseMap();

            CreateMap<RoleFeature, AddFeatureDTO>()
             .ForMember(dest => dest.featureId, opt => opt.MapFrom(src => src.Feature.featureId))
             .ForMember(dest=>dest.roleId, opt=>opt.MapFrom(src=>src.RoleFeatureId)) 
             .ForMember(dest => dest.view, opt => opt.MapFrom(src => src.view))
             .ForMember(dest => dest.add, opt => opt.MapFrom(src => src.add))
             .ForMember(dest => dest.edit, opt => opt.MapFrom(src => src.edit))
             .ForMember(dest => dest.delete, opt => opt.MapFrom(src => src.delete)).ReverseMap();
        




        }

    }
}
*/