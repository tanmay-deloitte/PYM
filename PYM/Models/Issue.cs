using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PYM.Models;

namespace PYM.models;
public class Issue{
    public Issue(){
        this.Labels = new HashSet<Label>();
    }
    [Key]
    public int IssueId{get;set;}
    public int ProjectId{get;set;}
    public string Type{get;set;}
    public string Title{get;set;}
    public string Description{get;set;}
    public virtual User Reporter{get;set;}
    public virtual User? Assignee {get;set;}
    public string Status{get;set;}
    [JsonIgnore]
    public virtual Project Project{get;set;}
    public ICollection<Label> Labels{get;set;}
}