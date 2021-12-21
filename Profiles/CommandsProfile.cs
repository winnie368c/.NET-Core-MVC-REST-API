using AutoMapper;
using Commander.Dtos;
using Commander.Models;

namespace Commander.Profiles
{
    //mapping command model to dtos
    //inherit base class Profile from AutoMapper namespace
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //Source -> Target
            //automapper maps from command to read dto
            CreateMap<Command, CommandReadDto>();

            //mapping created dto to command object
            CreateMap<CommandCreateDto, Command>();

            CreateMap<CommandUpdateDto, Command>();

            CreateMap<Command, CommandUpdateDto>();
        }

    }
    
}