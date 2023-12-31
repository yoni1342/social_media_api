using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Galacticos.Application.DTOs;
using Galacticos.Domain.Entities;

namespace Galacticos.Application.Persistence.Contracts;
public interface IUserRepository
{
    User? GetUserByIdentifier(string identifier);   
    User? GetUserById(Guid id);
    User? GetUserByEmail(string email);
    User? GetUserByUserName(string userName);
    void AddUser(User user);
    User EditUser(User user);
    List<User> GetAllUsers();
    Task<bool> Exists(Guid id);
}
