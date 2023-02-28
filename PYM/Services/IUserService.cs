using PYM.models;
using PYM.Models;

namespace PYM.Services;
public interface IUserService{
    ResponseModel SaveUser(UserRequest User);
}