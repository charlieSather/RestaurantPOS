using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestaurantPosApp.Models;

namespace RestaurantPosApp.Contracts
{
    public interface IIngredientRepository : IRepositoryBase<Ingredient>
    {
        void CreateIngredient(Ingredient ingredient);
        void AddListOfIngredients(List<Ingredient> ingredients);
        void UpdateIngredient(Ingredient ingredient);
        void DeleteIngredient(Ingredient ingredient);
        IQueryable<Ingredient> GetIngredients();
        IEnumerable<Ingredient> GetIngredients(IEnumerable<int> ids);
        Ingredient GetIngredient(int id);
        Ingredient GetIngredientAsNoTracking(int id);
    }
}
