using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class EmployeeRepository : RepositoryBase<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(ApplicationDbContext applicationDbContext)
            :base(applicationDbContext)
        {
        }

        public void CreateEmployee(Employee employee) => Create(employee);

        public void DeleteEmployee(Employee employee) => Delete(employee);
        public void UpdateEmployee(Employee employee) => Update(employee);
        public Employee GetEmployee(int id) => FindByCondition(x => x.EmployeeId == id).FirstOrDefault();

        public Employee GetEmployee(string userId) => FindByCondition(x => x.UserId == userId).FirstOrDefault();

    }
}
