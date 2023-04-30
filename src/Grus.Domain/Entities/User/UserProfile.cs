﻿namespace Grus.Domain.Entities.User;

public class UserProfile
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public List<Budget.Budget> Budgets { get; set; }
}
