using AutoMapper;
using Data.Models;
using Web.Models;

namespace Web.Common.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, EditCarViewModel>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Model.Make.Name))
                .ForMember(dest => dest.OwnerFirstName, opt => opt.MapFrom(src => src.Owner.FirstName))
                .ForMember(dest => dest.OwnerLastName, opt => opt.MapFrom(src => src.Owner.LastName));

            CreateMap<Car, CarIndexViewModel>()
                .ForMember(dest => dest.Model, opt => opt.MapFrom(src => src.Model.Name))
                .ForMember(dest => dest.Make, opt => opt.MapFrom(src => src.Model.Make.Name));

            CreateMap<AddCarViewModel, Car>()
                .ForMember(x => x.Model, opt => opt.Ignore())
                .ForMember(x => x.Owner, opt => opt.Ignore());

            CreateMap<AddCarViewModel, Owner>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.OwnerFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.OwnerLastName));

            CreateMap<EditCarViewModel, Car>()
               .ForMember(x => x.Model, opt => opt.Ignore())
               .ForMember(x => x.Owner, opt => opt.Ignore());

            CreateMap<EditCarViewModel, Owner>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.OwnerFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.OwnerLastName));
        }
    }
}
