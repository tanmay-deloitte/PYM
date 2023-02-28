using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PYM.models;

namespace PYM.Models;
public class Role{
    public Role(){
        this.Users = new HashSet<User>();
    }
    [Key]
    public int RoleId{get;set;}
    public string RoleName{get;set;}
    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; }
}