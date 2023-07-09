using AgendamentoDeTarefas.Models;
using Microsoft.EntityFrameworkCore;

namespace AgendamentoDeTarefas.Context
{
    public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        {}
        public DbSet<Tarefa> Tarefas { get; set; }  
    }
}
