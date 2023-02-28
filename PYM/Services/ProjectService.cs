using Microsoft.EntityFrameworkCore;
using PYM.models;
using PYM.Models;

namespace PYM.Services;
public class ProjectService : IProjectService
{
    private PYMContext _context;
    public ProjectService(PYMContext context){
        _context=context;
    }
    public ResponseModel DeleteProject(int projectId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Project _temp = GetProjectById(projectId);
            if (_temp != null) {
                _context.Remove < Project > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Project Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Project Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public List<Project> GetAllProjects()
    {
        List < Project > projects;
        try {
            projects = _context.Project.Include(s=>s.Issue).Include(s=>s.Creator).ToList();
        } catch (Exception) {
            throw;
        }
        return projects;
    }

    public ICollection<Issue> GetIssuesByProjectId(int projectId)
    {
        ICollection<Issue> issues;
        try{
            issues =_context.Project.Include(s=>s.Issue).Include(s=>s.Creator).SingleOrDefault(s=>s.ProjectId==projectId).Issue;
        } catch (Exception){
            throw;
        }
        return issues;
    }

    public Project GetProjectById(int projectId)
    {
        Project project;
        try {
            project =_context.Project.Include(s=>s.Issue).Include(s=>s.Creator).SingleOrDefault(s=>s.ProjectId==projectId);
            // project = _context.Project.SingleOrDefault(project=>project.ProjectId==projectId);
        } catch (Exception) {
            throw;
        }
        return project;
    }

    public ResponseModel SaveProject(ProjectRequest project)
    {
        ResponseModel model = new ResponseModel();
        try {
                User user = _context.Find<User>(project.CreatorId);
                if(user == null){
                    model.IsSuccess = false;
                    model.Messsage = "User Not Found";
                }
                else{
                    Project proj = new Project(){
                    Description = project.Description,
                    Creator = user
                    };
                    _context.Add < Project > (proj);
                    model.Messsage = "Project Inserted Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel UpdateProject(int projectId, string description)
    {
        ResponseModel model = new ResponseModel();
        try {
                Project proj = _context.Find<Project>(projectId);
                if(proj == null){
                    model.IsSuccess = false;
                    model.Messsage = "Project Not Found";
                }
                else{
                    proj.Description = description;
                    model.Messsage = "Project Updated Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
}
