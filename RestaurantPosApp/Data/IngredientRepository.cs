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
        public IEnumerable<Ingredient> GetIngredients(IEnumerable<int> IngredientIds)
        {
            var ingredients = FindAll();

            return IngredientIds.Join(FindAll(), id => id, ingredient => ingredient.IngredientId, (id, ingredient) => ingredient);

            //var query = from id in IngredientIds
            //            join item in ingredients on id equals item.IngredientId
            //            select item;

            //return query;

            //return ingredients.Join(IngredientIds, ingredient => ingredient.IngredientId, ingredientId => ingredientId, (ingredient, ingredientId) => ingredient);
        }

    }
}
