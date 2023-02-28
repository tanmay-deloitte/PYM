using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PYM.models;
namespace PYM.Models;
public class Label{
    public Label(){
        this.Issues = new HashSet<Issue>();
    }
    [Key]
    public int LabelId{get;set;}
    public string LabelName{get;set;}
    [JsonIgnore]
    public ICollection<Issue> Issues{get;set;}
}