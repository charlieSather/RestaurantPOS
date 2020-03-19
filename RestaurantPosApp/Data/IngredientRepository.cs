using RestaurantPosApp.Contracts;
using RestaurantPosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantPosApp.Data
{
    public class IngredientRepository : RepositoryBase<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(ApplicationDbContext applicationDbContext)
            : base(applicationDbContext)
        {
        }

        public void CreateIngredient(Ingredient ingredient) => Create(ingredient);
        public void DeleteIngredient(Ingredient ingredient) => Delete(ingredient);
        public void UpdateIngredient(Ingredient ingredient) => Update(ingredient);
        public Ingredient GetIngredient(int id) => FindByCondition(x => x.IngredientId == id).FirstOrDefault();
        public IQueryable<Ingredient> GetIngredients() => FindAll();
    }
}
