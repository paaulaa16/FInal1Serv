#define SQL_NO
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Influencers.Models;

public class ModeloContext : DbContext
{
    public static string connString { get; private set; } 
    
    public ModeloContext()
    {
        var database = "F1BDPaula";
        connString = $"Server=185.60.40.210\\SQLEXPRESS,58015;Database={database};User Id=sa;Password=Pa88word;MultipleActiveResultSets=true";
    
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(connString);
    
    public DbSet<Influencers.Models.Influencer> Influencer { get; set; }

    public DbSet<Influencers.Models.Producto> Producto { get; set; }

    public DbSet<Influencers.Models.Contrato> Contrato { get; set; }
}
