using Microsoft.EntityFrameworkCore;
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
        public void AddListOfIngredients(List<Ingredient> ingredients) => AddRange(ingredients);
        public void DeleteIngredient(Ingredient ingredient) => Delete(ingredient);
        public void UpdateIngredient(Ingredient ingredient) => Update(ingredient);
        public Ingredient GetIngredientAsNoTracking(int id) => FindByCondition(x => x.IngredientId == id).AsNoTracking().FirstOrDefault();
        public Ingredient GetIngredient(int id) => FindByCondition(x => x.IngredientId == id).FirstOrDefault();
        public IQueryable<Ingredient> GetIngredients() => FindAll();
        public IEnumerable<Ingredient> GetIngredients(IEnumerable<int> IngredientIds)
        {
            return IngredientIds.Join(FindAll(), id => id, ingredient => ingredient.IngredientId, (id, ingredient) => ingredient);
        }

    }
}
