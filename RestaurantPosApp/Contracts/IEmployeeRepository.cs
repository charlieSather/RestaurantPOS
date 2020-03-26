using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IEmployeeRepository : IRepositoryBase<Employee>
    {
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Employee GetEmployee(int id);
        Employee GetEmployee(string userId);
    }
}
