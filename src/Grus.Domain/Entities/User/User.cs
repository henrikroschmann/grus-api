﻿namespace Grus.Domain.Entities.User;

public class User
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}