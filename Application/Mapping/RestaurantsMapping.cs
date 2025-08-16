using Application.DTOs;
using Application.Features.Restaurants.Commands.Create;
using Application.Features.Restaurants.Commands.Update;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class RestaurantsMapping : Profile
    {
        public RestaurantsMapping()
        {
            CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(c => c.City, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.City))
                .ForMember(c => c.PostalCode, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.PostalCode))
                .ForMember(c => c.Street, opt => opt.MapFrom(src => src.Address == null ? null : src.Address.Street))
                .ForMember(c => c.Dishes, opt => opt.MapFrom(src => src.Dishes));

            CreateMap<UpdateRestaurantCommand, Restaurant>();
            CreateMap<CreateRestaurantCommand, Restaurant>().ForMember(a => a.Address, opt => opt.MapFrom(
                srce => new Address
                {
                    City = srce.City,
                    Street = srce.Street,
                    PostalCode = srce.PostalCode
                }
                ));
        }
    }
}
