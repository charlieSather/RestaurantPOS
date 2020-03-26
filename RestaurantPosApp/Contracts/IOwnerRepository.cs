using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IOwnerRepository : IRepositoryBase<Owner>
    {
        void CreateOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
        Owner GetOwner(int id);
        Owner GetOwner(string userId);

    }
}
