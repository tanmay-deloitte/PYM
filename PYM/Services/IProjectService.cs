using PYM.models;
using PYM.Models;

namespace PYM.Services;
public interface IProjectService{
    List<Project> GetAllProjects();
    Project GetProjectById(int projectId);
    ICollection<Issue> GetIssuesByProjectId(int projectId);
    ResponseModel SaveProject(ProjectRequest project);
    ResponseModel UpdateProject(int projectId,string description);
    ResponseModel DeleteProject(int projectId);

}