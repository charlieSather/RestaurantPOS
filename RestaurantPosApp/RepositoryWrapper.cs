using RestaurantPosApp.Contracts;
using RestaurantPosApp.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private ApplicationDbContext _context;

        public RepositoryWrapper(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
