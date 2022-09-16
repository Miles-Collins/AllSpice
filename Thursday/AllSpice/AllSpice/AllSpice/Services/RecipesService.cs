using System;
using System.Collections.Generic;
using AllSpice.Models;

namespace AllSpice.Controllers
{
  public class RecipesService
  {
    private readonly RecipesRepository _recipesRepo;

    public RecipesService(RecipesRepository recipesRepo)
    {
      _recipesRepo = recipesRepo;
    }

    internal List<Recipe> GetAll()
    {
      return _recipesRepo.GetAll();
    }

    internal Recipe Create(Recipe newRecipe)
    {
      return _recipesRepo.Create(newRecipe);
    }

    internal Recipe GetById(int id)
    {
      Recipe recipe = _recipesRepo.GetById(id);
      if (recipe == null)
      {
        throw new Exception("There is no recipe by that Id.");
      }
      return recipe;
    }

    internal object Update(Recipe update)
    {
      Recipe original = GetById(update.Id);
      original.Category = update.Category ?? original.Category;
      original.Creator = update.Creator ?? original.Creator;
      original.Title = update.Title ?? original.Title;
      original.Picture = update.Picture ?? original.Picture;
      return _recipesRepo.Update(original);
    }

    internal string Delete(int id)
    {
      Recipe recipe = GetById(id);
      _recipesRepo.Delete(id);
      return $"Deleted {recipe.Title}";
    }
  }
}