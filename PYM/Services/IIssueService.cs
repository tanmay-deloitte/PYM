using PYM.models;
using PYM.Models;

namespace PYM.Services;
public interface IIssueService{
    List<Issue> GetAllIssues();
    Issue GetIssueById(int issueId);
    List<Issue> SearchIssueOnProjectOrAssignee(int ProjectId,string Email);
    List<Issue> SearchIssueOnProjectAndAssignee(int ProjectId,string Email);
    List<Issue> SearchIssueOnTitleAndDescription(string Title,string Description);
    List<Issue> SearchIssueOnType(string Type);
    ResponseModel SaveIssue(IssueRequest issue);
    ResponseModel UpdateIssue(int issueId,IssueUpdateRequest issue);
    ResponseModel UpdateStatus(int issueId,string status);
    ResponseModel AssignIssueToUser(int issueId,int userId);
    ResponseModel DeleteIssue(int issueId);
}