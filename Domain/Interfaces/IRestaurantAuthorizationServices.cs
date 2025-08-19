using Domain.Entities;
using Domain.Enums;

namespace Domain.Interfaces
{
    public interface IRestaurantAuthorizationServices
    {
        bool Authorize(Restaurant restaurant, ResourceOperationEnum resourceOperationEnum);
    }
}
