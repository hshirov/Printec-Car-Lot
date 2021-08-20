using AutoMapper;
using Data.Models;
using Web.Models;

namespace Web.Common.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<AddCarViewModel, Car>()
                .ForMember(x => x.Model, opt => opt.Ignore())
                .ForMember(x => x.Owner, opt => opt.Ignore());

            CreateMap<Car, CarIndexViewModel>();

            CreateMap<AddCarViewModel, Owner>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.OwnerFirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.OwnerLastName));
        }
    }
}
