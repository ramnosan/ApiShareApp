using AspServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AspServer;
public class Mutation
{
    public async Task<User> SaveUserAsync([Service] ShareDb db, User newUser)
    {   
        db.Users.Add(newUser);
        await db.SaveChangesAsync();
        return newUser;
    }
    public async Task<String> ChangePasswordAsync([Service] ShareDb db, string newPassword, int id)
    {
        var erg = db.Users.Find(id);
        if (erg != null)
        {
            erg.Password = newPassword;
        }
        else return "";
        await db.SaveChangesAsync();
        return erg.Password;
    }
    public async Task<IResult> Login([Service] ShareDb db, string name, string password)
    {
        var foundUser = await db.Users.FirstOrDefaultAsync(u => u.Name == name);
        if (foundUser == null) { }
        else if (foundUser.Password == password)
        {
            return Results.Ok();
        }
        return Results.Problem();
    }
}