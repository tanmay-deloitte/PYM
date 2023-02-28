using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PYM.Services;
using PYM.Models;

namespace PYM.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController:ControllerBase{
    IUserService _userService;
    ILogger<UserController> _logger;
    public UserController(IUserService service,ILogger<UserController> logger) {
        _userService = service;
        _logger = logger;
    }    
    
    [HttpPost]
    [Route("[action]")]
    public IActionResult SignUp(UserRequest userModel) {
        try {
            var model = _userService.SaveUser(userModel);
            _logger.LogInformation("User Created");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}