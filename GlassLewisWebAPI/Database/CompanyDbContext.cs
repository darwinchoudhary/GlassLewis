using System;
using GlassLewisWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GlassLewisWebAPI.Database;

public class CompanyDbContext : DbContext{
public CompanyDbContext(DbContextOptions<CompanyDbContext> options)
        : base(options)
    { }

    public DbSet<Company> Companies { get; set; }
}
