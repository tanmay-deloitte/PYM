namespace PYM.models;
public class IssueRequest{
    public int ProjectId{get;set;}
    public string Type{get;set;}
    public string Title{get;set;}
    public string Description{get;set;}
    public int ReporterId{get;set;}
    public string Status{get;set;}
}