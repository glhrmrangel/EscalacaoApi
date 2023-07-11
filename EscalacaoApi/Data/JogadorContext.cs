using EscalacaoApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EscalacaoApi.Data;

public class JogadorContext : DbContext
{
    public JogadorContext(DbContextOptions<JogadorContext> options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", false, true).Build();

        optionsBuilder.UseSqlServer(configuration.GetConnectionString("EscalacaoConnection"));
    }

    public DbSet<Jogador> Jogadores { get; set; }
    public DbSet<Treinador> Treinadores { get; set; }
    public DbSet<Time> Times { get; set; }
}
