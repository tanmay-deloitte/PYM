using Microsoft.EntityFrameworkCore;
using PYM.models;
using PYM.Models;

namespace PYM.Services;
public class UserService : IUserService
{
    private PYMContext _context;
    public UserService(PYMContext context){
        _context=context;
    }

    public ResponseModel SaveUser(UserRequest User)
    {
        ResponseModel model = new ResponseModel();
        try {
                User user = new User(){
                    Name = User.Name,
                    UserName = User.UserName,
                    Email = User.Email,
                    Password = User.Password,
                };
                _context.Add < User > (user);
                model.Messsage = "User Inserted Successfully";
            _context.SaveChanges();
            model.IsSuccess = true;
        } catch (Exception ex) {
            model.IsSuccess = false;
            model.Messsage = "Error : " + ex.Message;
        }
        return model;
    }
}
