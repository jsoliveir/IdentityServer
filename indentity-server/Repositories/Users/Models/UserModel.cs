﻿
namespace IdentityServer.Repositories.Users;
public class UserModel
{
    public int Id { get; set; }
    public string? Username { get; set; }
    public string? Password { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public bool IsActive { get; set; }
}
