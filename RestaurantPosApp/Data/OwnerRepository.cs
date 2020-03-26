using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class OwnerRepository : RepositoryBase<Owner>, IOwnerRepository
    {
        public OwnerRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }

        public void CreateOwner(Owner owner) => Create(owner);
        public void DeleteOwner(Owner owner) => Delete(owner);
        public void UpdateOwner(Owner owner) => Update(owner);


        public Owner GetOwner(int id) => FindByCondition(x => x.OwnerId == id).FirstOrDefault();

        public Owner GetOwner(string userId) => FindByCondition(x => x.UserId == userId).FirstOrDefault();

    }
}
