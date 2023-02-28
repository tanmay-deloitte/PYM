using PYM.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PYM.models;

namespace PYM.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class ProjectController:ControllerBase{
    IProjectService _projectService;
    IIssueService _issueService;
    ILogger<ProjectController> _logger;
    public ProjectController(IProjectService projectService,IIssueService issueService,ILogger<ProjectController> logger) {
        _projectService = projectService;
        _issueService = issueService;
        _logger = logger;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult GetAllProjects() {
        try {
            var projects = _projectService.GetAllProjects();
            if (projects == null) return NotFound();
            _logger.LogInformation("Getting All Projects");
            return Ok(projects);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/id")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult GetProjectById(int id) {
        try {
            var project = _projectService.GetProjectById(id);
            if (project == null) return NotFound();
            _logger.LogInformation("Getting project of id- {0}",id);
            return Ok(project);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/id")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult GetIssuesByProjectId(int id) {
        try {
            var issues = _projectService.GetIssuesByProjectId(id);
            if (issues.Count == 0) return NotFound();
            _logger.LogInformation("Getting issues of project- {0}",id);
            return Ok(issues);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult CreateProject(ProjectRequest projectModel) {
        try {
            var model = _projectService.SaveProject(projectModel);
            _logger.LogInformation("Project Created");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult UpdateProject(int projectId,string description) {
        try {
            var model = _projectService.UpdateProject(projectId,description);
            _logger.LogInformation("Project Updated");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="admin")]
    public IActionResult DeleteProject(int id) {
        try {
            var model = _projectService.DeleteProject(id);
            _logger.LogInformation("Project Deleted");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult CreateIssueUnderProject(IssueRequest issueModel) {
        try {
            var model = _issueService.SaveIssue(issueModel);
            _logger.LogInformation("Issue Created Under project- {0}",issueModel.ProjectId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult UpdateIssueUnderProject(int issueId,IssueUpdateRequest issueModel) {
        try {
            var model = _issueService.UpdateIssue(issueId,issueModel);
            _logger.LogInformation("Issue Updated Under project");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult DeleteIssueUnderProject(int id) {
        try {
            var model = _issueService.DeleteIssue(id);
            _logger.LogInformation("Issue Deleted Under project");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

}