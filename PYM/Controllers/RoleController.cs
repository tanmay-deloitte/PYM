using PYM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PYM.models;
using PYM.Models;

namespace PYM.Controllers;

[ApiController]
[Route("[controller]")]
// [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class RoleController:ControllerBase{
    IRoleService _roleService;
    ILogger<RoleController> _logger;
    public RoleController(IRoleService service,ILogger<RoleController> logger) {
        _roleService = service;
        _logger = logger;
    }
    
    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult CreateRole(RoleRequest roleModel) {
        try {
            var model = _roleService.AddRole(roleModel);
            _logger.LogInformation("Role Added");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult AssignRoleToUser(int UserId, int RoleId) {
        try {
            var model = _roleService.AssignRole(UserId,RoleId);
            _logger.LogInformation("Role Assigned to user- {0}",UserId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}