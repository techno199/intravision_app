using Microsoft.AspNetCore.Mvc;
using server.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace server.Controllers
{
  public class AccountController : ControllerBase
  {
    private readonly DbService _dbService;
    public AccountController(DbService dbService)
    {
      _dbService = dbService;
    }

    // Authorize
    [HttpGet]
    public async Task<ActionResult> Login(
      [FromQuery]int deviceId,
      [FromQuery]string code
    )
    {
      if (await _dbService.IsAuthorized(deviceId, code))
      {
        await Authenticate(deviceId);
        return Ok();
      }
      return Unauthorized();
    }

    [Authorize]
    public async Task Logout()
    {
      await HttpContext.SignOutAsync();
    }

    [Authorize]
    public void IsLoggedIn()
    {
    }

    private async Task Authenticate(int deviceId)
    {
      var claims = new List<Claim>() 
      {
        new Claim(ClaimsIdentity.DefaultNameClaimType, deviceId.ToString())
      };
      ClaimsIdentity id = new ClaimsIdentity(
        claims,
        "ApplicationCookie",
        ClaimsIdentity.DefaultNameClaimType,
        ClaimsIdentity.DefaultRoleClaimType
      );
      await HttpContext.SignInAsync(
        CookieAuthenticationDefaults.AuthenticationScheme,
        new ClaimsPrincipal(id)
      );
    }
  }
}