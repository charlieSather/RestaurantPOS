using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Contracts
{
    public interface IEmailService
    {
        Task<bool> EmailAsync(Owner owner, string htmlContent);
    }
}
