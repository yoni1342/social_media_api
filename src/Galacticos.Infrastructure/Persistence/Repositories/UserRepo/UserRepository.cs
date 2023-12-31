using System;
using System.Collections.Generic;
using System.Linq;
using Galacticos.Application.Persistence.Contracts;
using Galacticos.Domain.Entities;
using Galacticos.Infrastructure.Data;

namespace Galacticos.Infrastructure.Persistence.Repositories.UserRepo;

public class UserRepository : IUserRepository
{
    private static readonly List<User> _users = new();
    private readonly ApiDbContext _context;

    public UserRepository(ApiDbContext context)
    {
        _context = context;
    }

    public void AddUser(User user)
    {
        _context.users.Add(user);
        if (_context.SaveChanges() == 0)
            throw new Exception("User not added");
    }

    public User? GetUserByIdentifier(string identifier)
    {
        Console.WriteLine(identifier);
        return _context.users.FirstOrDefault(u => u.Email == identifier || u.UserName == identifier);
    }

    public User? GetUserByEmail(string email)
    {
        return _context.users.FirstOrDefault(u => u.Email == email);
    }

    public User? GetUserById(Guid id)
    {
        return _context.users.FirstOrDefault(u => u.Id == id);
    }

    public User? GetUserByUserName(string userName)
    {
        return _context.users.FirstOrDefault(u => u.UserName == userName);
    }

    public User EditUser(User user)
    {
        var userToEdit = _context.users.FirstOrDefault(u => u.Id == user.Id);
        if (userToEdit == null)
            throw new Exception("User not found");
        userToEdit.FirstName = user.FirstName;
        userToEdit.LastName = user.LastName;
        userToEdit.Email = user.Email;
        userToEdit.UserName = user.UserName;
        userToEdit.Bio = user.Bio;
        userToEdit.Picture = user.Picture;
        userToEdit.Password = user.Password;

        if (_context.SaveChanges() == 0)
            throw new Exception("User not edited");
        
        return userToEdit;
    }

    public List<User> GetAllUsers()
    {
        return _context.users.ToList();
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _context.users.FindAsync(id) != null;
    }
}