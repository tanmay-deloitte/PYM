using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PYM.Services;
using PYM.models;

namespace PYM.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)] 
public class LabelController:ControllerBase{
    ILabelService _labelService;
    ILogger<LabelController> _logger;
    public LabelController(ILabelService service,ILogger<LabelController> logger) {
        _labelService = service;
        _logger = logger;
    }

    [HttpGet]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult GetAllLabels() {
        try {
            var labels = _labelService.GetAllLabels();
            if (labels == null) return NotFound();
            _logger.LogInformation("Getting All Labels");
            return Ok(labels);
        } catch (Exception e) {
            return BadRequest();
        }
    }

    [HttpGet]
    [Route("[action]/id")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult GetLabelById(int id) {
        try {
            var label = _labelService.GetLabelById(id);
            if (label == null) return NotFound();
            _logger.LogInformation("Getting Labels of id- {0}",id);
            return Ok(label);
        } catch (Exception) {
            return BadRequest();
        }
    }
    
    [HttpPost]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult CreateLabel(string labelName) {
        try {
            var model = _labelService.CreateLabel(labelName);
            _logger.LogInformation("Label Created");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult UpdateLabel(int labelId,string labelName) {
        try {
            var model = _labelService.UpdateLabel(labelId,labelName);
            _logger.LogInformation("Label Updated");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult AddLabelToIssue(int issueId,int labelId) {
        try {
            var model = _labelService.AddLabelToIssue(issueId,labelId);
            _logger.LogInformation("Label Added to issue- {0}",issueId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpPut]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult RemoveLabelFromIssue(int issueId,int labelId) {
        try {
            var model = _labelService.RemoveLabelFromIssue(issueId,labelId);
            _logger.LogInformation("Label removed from issue- {0}",issueId);
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }

    [HttpDelete]
    [Route("[action]")]
    [Authorize(Roles="admin,projectManager,standard")]
    public IActionResult DeleteLabel(int id) {
        try {
            var model = _labelService.DeleteLabel(id);
            _logger.LogInformation("Label Deleted");
            return Ok(model);
        } catch (Exception) {
            return BadRequest();
        }
    }
}