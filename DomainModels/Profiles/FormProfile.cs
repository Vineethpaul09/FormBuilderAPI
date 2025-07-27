using AutoMapper;
using FormBuilderAPI.DisplayClasses;
using FormBuilderAPI.Data.Entities;

namespace FormBuilderAPI.DomainModels.Profiles
{
    public class FormProfile : Profile
    {
        public FormProfile()
        {
            // Map FormRequest -> Form
            CreateMap<FormRequest, Form>()
                .ForMember(dest => dest.TenantId, opt => opt.MapFrom(src => src.TenantId)) // Map TenantId
                .ForMember(dest => dest.Fields, opt => opt.MapFrom(src => src.Fields));

            // Map FormFieldRequest -> FormSection
            CreateMap<FormFieldRequest, FormSection>()
                .ForMember(dest => dest.Alignment, opt => opt.MapFrom(src => src.Alignment))
                .ForMember(dest => dest.ColumnsPerRow, opt => opt.MapFrom(src => src.ColumnsPerRow))
                .ForMember(dest => dest.Columns, opt => opt.MapFrom(src => src.Columns));

            // Map FormColumnRequest -> FormField
            CreateMap<FormColumnRequest, FormField>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.Label, opt => opt.MapFrom(src => src.Label))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Placeholder, opt => opt.MapFrom(src => src.Placeholder))
                .ForMember(dest => dest.Required, opt => opt.MapFrom(src => src.Required))
                .ForMember(dest => dest.Options, opt => opt.MapFrom(src => src.Options))
                .ForMember(dest => dest.FormSection, opt => opt.Ignore()); // Ignore FormSection as it will be set by the parent FormSection

          
        }
    }
}