using System;
using Microsoft.AspNetCore.Identity;
namespace rosterapi.Models;


public class User : IdentityUser
{
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    public bool IsActive { get; set; }
}

