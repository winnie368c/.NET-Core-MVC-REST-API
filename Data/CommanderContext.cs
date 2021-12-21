using Commander.Models;
using Microsoft.EntityFrameworkCore; //object-database mapper

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        //calling base class constructor 
        public CommanderContext(DbContextOptions<CommanderContext> opt) : base(opt)
        {
            
        }

        //creating command model in db; putting command object in db as a dbset called Commands
        //'Commands' - name of table in migration
        public DbSet<Command> Commands { get; set; }

    }
}