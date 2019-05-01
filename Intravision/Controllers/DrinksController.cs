using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using server.Models;
using server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace server.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class DrinksController : ControllerBase
  {
    private readonly Context _context;
    private readonly DbService _dbService;
    public DrinksController(
      Context context,
      DbService dbService) 
    {
      _context = context;
      _dbService = dbService;

    }
    [HttpGet]
    public async Task<List<Drink>> Get (
      [FromQuery]int deviceId
    )
    {
      return await _dbService.GetDrinks(deviceId);
    }

        
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Drink>> Post(
      [FromBody]Drink drink
    )
    {
      var deviceId = Int32.Parse(User.Identity.Name);
      var newDrink = await _dbService.CreateDrink(drink, deviceId);
      if (newDrink != null)
        return newDrink;
      return BadRequest();
    }

    [HttpDelete]
    [Authorize]
    public async void Delete(
        [FromQuery]int drinkId
    )
    {
        await _dbService.DeleteDrink(drinkId);
    }

    [HttpPut]
    [Authorize]
    public async Task<Drink> Put(
        [FromBody]Drink drink
    )
    {
        return await _dbService.UpdateDrink(drink);
    }

    [HttpGet]
    [Route("[action]")]
    public void BuyDrink(
        [FromQuery]int drinkId,
        [FromQuery]int amount
    )
    {
        _dbService.BuyDrink(drinkId, amount);
    }
  }
}
