using Microsoft.EntityFrameworkCore;
using PYM.models;
using PYM.Models;

namespace PYM.Services;
public class IssueService : IIssueService
{
    private PYMContext _context;
    public IssueService(PYMContext context){
        _context=context;
    }

    public ResponseModel AssignIssueToUser(int issueId, int userId)
    {
        ResponseModel model = new ResponseModel();
        try {
                Issue issue = _context.Find<Issue>(issueId);
                User user = _context.Find<User>(userId);
                if(issue==null){
                    model.IsSuccess = false;
                    model.Messsage = "Issue Not Found";
                }
                else if (user ==null){
                    model.IsSuccess = false;
                    model.Messsage = "User Not Found";
                }
                else{
                    issue.Assignee = user;
                    model.Messsage = "Issue Assigned Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;        
    }

    public ResponseModel DeleteIssue(int issueId)
    {
        ResponseModel model = new ResponseModel();
        try {
            Issue _temp = GetIssueById(issueId);
            if (_temp != null) {
                _context.Remove < Issue > (_temp);
                _context.SaveChanges();
                model.IsSuccess = true;
                model.Messsage = "Issue Deleted Successfully";
            } else {
                model.IsSuccess = false;
                model.Messsage = "Issue Not Found";
            }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public List<Issue> GetAllIssues()
    {
        List < Issue > Issues;
        try {
            Issues = _context.Issue.Include(s=>s.Project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).ToList();
        } catch (Exception) {
            throw;
        }
        return Issues;
    }

    public Issue GetIssueById(int issueId)
    {
        Issue Issue;
        try {
            Issue = _context.Issue.Include(s=>s.Project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).SingleOrDefault(s=>s.IssueId==issueId);
            // Issue = _context.Find < Issue > (issueId);
        } catch (Exception) {
            throw;
        }
        return Issue;
    }

    public ResponseModel SaveIssue(IssueRequest Issue)
    {
        ResponseModel model = new ResponseModel();
        try {
                bool typeExists,statusExists;
                typeExists = Enum.IsDefined(typeof(EnumIssueType), Issue.Type);
                statusExists = Enum.IsDefined(typeof(EnumStatus), Issue.Status);
                if(typeExists==false){
                    model.Messsage = "Issue type does not exist";
                    model.IsSuccess = false;
                }
                else if(statusExists==false){
                    model.Messsage = "Status does not exist";
                    model.IsSuccess = false;
                }
                else{
                    Project project = _context.Find<Project>(Issue.ProjectId);
                    User reporter = _context.Find<User>(Issue.ReporterId);
                    Issue issue = new Issue(){
                    ProjectId = Issue.ProjectId,
                    Type = Issue.Type,
                    Title = Issue.Title,
                    Description = Issue.Description,
                    Reporter = reporter,
                    Status = Issue.Status,
                    Project= project
                };
                    _context.Add < Issue > (issue);
                    model.Messsage = "Issue Inserted Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public List<Issue> SearchIssueOnProjectAndAssignee(int ProjectId, string Email)
    {
        List<Issue> Issue;
        try {
            Issue = _context.Issue.Include(s=>s.Project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).Where(s=>s.ProjectId == ProjectId && s.Assignee.Email == Email).ToList();
        } catch (Exception) {
            throw;
        }
        return Issue;
    }

    public List<Issue> SearchIssueOnProjectOrAssignee(int ProjectId, string Email)
    {
        List<Issue> Issue;
        try {
            Issue = _context.Issue.Include(s=>s.Project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).Where(s=>s.ProjectId == ProjectId || s.Assignee.Email == Email).ToList();
        } catch (Exception) {
            throw;
        }
        return Issue;
    }

    public List<Issue> SearchIssueOnTitleAndDescription(string Title, string Description)
    {
        List<Issue> Issue;
        try {
            Issue = _context.Issue.Include(s=>s.Project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).Where(s=>s.Title == Title && s.Description == Description).ToList();
        } catch (Exception) {
            throw;
        }
        return Issue;
    }

    public List<Issue> SearchIssueOnType(string Type)
    {
        List<Issue> Issue;
        try {
            Issue = _context.Issue.Include(s=>s.Project).Include(s=>s.Reporter).Include(s=>s.Assignee).Include(s=>s.Labels).Where(s=>s.Type == Type).ToList();
        } catch (Exception) {
            throw;
        }
        return Issue;
    }

    public ResponseModel UpdateIssue(int issueId, IssueUpdateRequest tempIssue)
    {
        ResponseModel model = new ResponseModel();
        try {
                bool exists;
                exists = Enum.IsDefined(typeof(EnumIssueType), tempIssue.Type);
                if(exists){
                    Issue issue = _context.Find<Issue>(issueId);
                    issue.Type = tempIssue.Type;
                    issue.Description = tempIssue.Description;
                    issue.Title = tempIssue.Title;
                    model.Messsage = "Issue Inserted Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
                else{
                    model.Messsage = "Issue type does not exist";
                    model.IsSuccess = false;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel UpdateStatus(int issueId, string status)
    {
        ResponseModel model = new ResponseModel();
        try {
                bool statusExists;
                statusExists = Enum.IsDefined(typeof(EnumStatus), status);
                if(statusExists==false){
                    model.Messsage = "Status does not exist";
                    model.IsSuccess = false;
                }
                else{
                    Issue issue = _context.Find<Issue>(issueId);
                    int c=0,pC=0;
                    foreach (string name in Enum.GetNames(typeof(EnumStatus)))  
                    {  
                        if (name==status){
                            break;
                        }
                        c=c+1;
                    }
                    foreach (string name in Enum.GetNames(typeof(EnumStatus)))  
                    {  
                        if(name==issue.Status)
                        {
                            break;
                        }
                        pC=pC+1;
                    }
                    if(c<=pC+1){
                        issue.Status = status;
                        model.Messsage = "Status Updated Successfully";
                        _context.SaveChanges();
                        model.IsSuccess = true;
                    }
                    else{
                        model.Messsage = "Status should be in sequence";
                        model.IsSuccess = false;
                    }
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
}
