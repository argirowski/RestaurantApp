using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class DishesMapping : Profile
    {
        public DishesMapping()
        {
            CreateMap<Dish, DishDTO>();
        }
    }
}
