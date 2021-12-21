using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    //interface with CRUD functionalities
    public interface ICommanderRepo
    {
        //saves data in db when changes are made via dbcontext
        bool SaveChanges();

        //gives list of all command objects
        IEnumerable<Command> GetAllCommands();
        Command GetCommandById(int id);
        void CreateCommand(Command cmd);
        void UpdateCommand(Command cmd);
        void DeleteCommand(Command cmd);
    }
}