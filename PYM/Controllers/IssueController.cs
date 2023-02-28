using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PYM.Services;
using PYM.models;

namespace PYM.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class IssueController:ControllerBase{
    IIssueService _issueService;
    ILogger<IssueController> _logger;
    public IssueController(IIssueService service,ILogger<IssueController> logger) {
        _issueService = service;
        _logger = logger;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult GetAllIssues() {
        try {
            var issues = _issueService.GetAllIssues();
            _logger.LogInformation("Getting All issues");
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult SearchIssueOnProjectAndAssignee(int ProjectId,string Email) {
        try {
            var issues = _issueService.SearchIssueOnProjectAndAssignee(ProjectId,Email);
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult SearchIssueOnProjectOrAssignee(int ProjectId,string Email) {
        try {
            var issues = _issueService.SearchIssueOnProjectOrAssignee(ProjectId,Email);
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult SearchIssueOnTitleAndDescription(string Title,string Description) {
        try {
            var issues = _issueService.SearchIssueOnTitleAndDescription(Title,Description);
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult SearchIssueOnType(string Type) {
        try {
            var issues = _issueService.SearchIssueOnType(Type);
            if (issues == null) return NotFound();
            return Ok(issues);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/id")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult GetIssueById(int id) {
        try {
            var issue = _issueService.GetIssueById(id);
            _logger.LogInformation("Getting issue of id - {0}",id);
            if (issue == null) return NotFound();
            return Ok(issue);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult CreateIssue(IssueRequest issueModel) {
        try {
            var model = _issueService.SaveIssue(issueModel);
            _logger.LogInformation("Issue Created");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult UpdateIssue(int issueId,IssueUpdateRequest issueModel) {
        try {
            var model = _issueService.UpdateIssue(issueId,issueModel);
            _logger.LogInformation("Issue Updated");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult UpdateStatus(int issueId,string status) {
        try {
            var model = _issueService.UpdateStatus(issueId,status);
            _logger.LogInformation("Status of issue - {0} updated",issueId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult AssignIssueToUser(int issueId,int userId) {
        try {
            var model = _issueService.AssignIssueToUser(issueId,userId);
            _logger.LogInformation("Assigned issue- {0} to user- {1}",issueId,userId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult DeleteIssue(int id) {
        try {
            var model = _issueService.DeleteIssue(id);
            _logger.LogInformation("Issue- {0} deleted",id);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}