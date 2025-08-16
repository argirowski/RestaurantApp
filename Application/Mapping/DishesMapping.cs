using Application.DTOs;
using Application.Features.Dishes.Commands.Create;
using AutoMapper;
using Domain.Entities;

namespace Application.Mapping
{
    public class DishesMapping : Profile
    {
        public DishesMapping()
        {
            CreateMap<Dish, DishDTO>();
            CreateMap<CreateDishCommand, Dish>();
        }
    }
}
