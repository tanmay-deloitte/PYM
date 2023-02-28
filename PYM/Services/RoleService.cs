using PYM.models;
using PYM.Models;

namespace PYM.Services;
public class RoleService : IRoleService
{
    PYMContext _context;
    public RoleService(PYMContext context){
        _context=context;
    }
    public ResponseModel AddRole(RoleRequest roleModel)
    {
        ResponseModel model = new ResponseModel();
        try {
                bool exists;
                exists = Enum.IsDefined(typeof(EnumRole), roleModel.RoleName);
                if(exists){
                    Role role = new Role(){
                    RoleName = roleModel.RoleName
                    };
                    _context.Add < Role > (role);
                    model.Messsage = "Role Inserted Successfully";
                    _context.SaveChanges();
                    model.IsSuccess = true;
                }
                else{
                    model.Messsage = "Role does not exist";
                    model.IsSuccess = false;
                }
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }

    public ResponseModel AssignRole(int UserId,int RoleId)
    {
        ResponseModel model = new ResponseModel();
        try {
                User user = _context.Find<User>(UserId);
                Role role = _context.Find<Role>(RoleId);
                if (user == null){
                    model.IsSuccess = false;
                    model.Messsage = "User Not Found";
                }
                else if (role == null){
                    model.IsSuccess = false;
                    model.Messsage = "Role Not Found";
                }
                else{
                    user.Roles.Add(role);
                    model.Messsage = "Role Assigned Successfully";
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