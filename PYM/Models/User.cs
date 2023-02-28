using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using PYM.Models;

namespace PYM.models;
public class User{
    public User(){
        this.Roles = new HashSet<Role>();
    }
    [Key]
    public int UserId{get;set;}
    public string Name{get;set;}
    public string UserName{get;set;}
    [JsonIgnore]
    public string Password{get;set;}
    public string Email{get;set;}
    [JsonIgnore]
    public virtual ICollection<Project> Projects{get;set;}
    [JsonIgnore]
    [InverseProperty("Reporter")]
    public virtual ICollection<Issue> IssuesCreated{get;set;}
    [JsonIgnore]
    [InverseProperty("Assignee")]
    public virtual ICollection<Issue> IssuesAssigned{get;set;}
    [JsonIgnore]
    public virtual ICollection<Role> Roles{get;set;}
}