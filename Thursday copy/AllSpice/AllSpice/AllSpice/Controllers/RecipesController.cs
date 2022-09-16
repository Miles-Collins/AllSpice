using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AllSpice.Models;
using Microsoft.AspNetCore.Mvc;
using CodeWorks.Auth0Provider;

namespace AllSpice.Controllers
{
  [ApiController]
  [Route("api/[controller]")]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesService _recipesService;

    public RecipesController(RecipesService recipesService)
    {
      _recipesService = recipesService;
    }

    [HttpGet]
    public ActionResult<List<Recipe>> GetAll()
    {
      try
      {
        List<Recipe> recipes = _recipesService.GetAll();
        return Ok(recipes);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPost]
    public async Task<ActionResult<Recipe>> Create([FromBody] Recipe newRecipe)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        newRecipe.CreatorId = userInfo.Id;
        Recipe recipe = _recipesService.Create(newRecipe);
        recipe.Creator = userInfo;
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpGet("{id}")]
    public ActionResult<Recipe> GetById(int id)
    {
      try
      {
        Recipe recipe = _recipesService.GetById(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Recipe>> UpdateAsync([FromBody] Recipe update, int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        update.Id = id;
        return Ok(_recipesService.Update(update));
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<string>> DeleteAsync(int id)
    {
      try
      {
        Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
        string recipe = _recipesService.Delete(id);
        return Ok(recipe);
      }
      catch (Exception e)
      {
        return BadRequest(e.Message);
      }
    }
  }
}