using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PYM.models;
public class Project{
    [Key]
    public int ProjectId{get;set;}
    public string Description{get;set;}
    public virtual ICollection<Issue> Issue{get;set;}
    public virtual User Creator{get;set;}
}