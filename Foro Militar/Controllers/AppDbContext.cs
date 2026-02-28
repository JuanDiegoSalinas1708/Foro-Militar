using Foro.Entities.Models;
using System.Collections.Generic;
using System.Data.Entity;

public class AppDbContext : DbContext
{
    public AppDbContext() : base("DefaultConnection")
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Post> Posts { get; set; }

    public DbSet<Community> Communities { get; set; }

    public DbSet<Comment> Comments { get; set; }

    public DbSet<Vote> Votes { get; set; }

    public DbSet<UserCommunity> UserCommunities { get; set; }
}