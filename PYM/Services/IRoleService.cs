using PYM.Models;

namespace PYM.Services;
public interface IRoleService{
    ResponseModel AddRole(RoleRequest model);
    ResponseModel AssignRole(int UserId,int RoleId);
}