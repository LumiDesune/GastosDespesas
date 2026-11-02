using GastosDespesas.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GastosDespesas.API.Data;
public class AppDbContext : DbContext {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Add entities here
    public DbSet<Despesa> Despesas => Set<Despesa>();
    public DbSet<Pessoa> Pessoas => Set<Pessoa>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    
}