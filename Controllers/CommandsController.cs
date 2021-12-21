using System.Collections.Generic;
using AutoMapper;
using Commander.Data;
using Commander.Dtos;
using Commander.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Commander.Controllers
{
    [Route("api/commands")] //entails how to get to API endpoints 
    [ApiController] //attribute that enables API behaviors like automatic HTTP 400 responses
    public class CommandsController : ControllerBase
    {
        //readonly allows variable to be calculated at runtime, not compile time
        private readonly ICommanderRepo _repository;
        private readonly IMapper _mapper;

        //using dependency injection in constructor on repository and mapper variables
        public CommandsController(ICommanderRepo repository, IMapper mapper)
        {
            _repository = repository; //assign injected variable to _repository
            _mapper = mapper;
        }
       
        //GET api/commands
        [HttpGet]
        [SwaggerOperation(Summary = "Gets all commands.")]
        //creating action result endpoint
        public ActionResult <IEnumerable<CommandReadDto>> GetAllCommmands()
        {
            var commandItems = _repository.GetAllCommands();

            //returning HTTP 200 OK result and commandItems mapped to Dto
            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commandItems));
        }

        //GET api/commands/{id}
        [HttpGet("{id}", Name="GetCommandById")]
        [SwaggerOperation(Summary = "Gets a specific command by id number.")]
        //id comes from [FromBody] due to [ApiController]
        public ActionResult <CommandReadDto> GetCommandById(int id)
        {
            var commandItem = _repository.GetCommandById(id);
            if(commandItem != null)
            {
                return Ok(_mapper.Map<CommandReadDto>(commandItem));
            }
            return NotFound();
        }

        //POST api/commands
        [HttpPost]
        [SwaggerOperation(Summary = "Creates a command with the three attributes 'howTo' (description of command), 'line' (the command), 'platform' (application platform).")]
        public ActionResult <CommandReadDto> CreateCommand(CommandCreateDto commandCreateDto)
        {
            //mapping commandCreateDto into command using automapper to put command model in database
            var commandModel = _mapper.Map<Command>(commandCreateDto);
            _repository.CreateCommand(commandModel);
            _repository.SaveChanges(); //creating object in the actual database

            //returning read dto
            var commandReadDto = _mapper.Map<CommandReadDto>(commandModel);

            //generate URL and HTTP 201 (principle of REST)
            return CreatedAtRoute(nameof(GetCommandById), new {Id = commandReadDto.Id}, commandReadDto);      
        }

        //PUT api/commands/{id}
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Replaces a specific command by id number with the new command.")]
        public ActionResult UpdateCommand(int id, CommandUpdateDto commandUpdateDto)
        {
            //checking if requested model exists
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }
            
            //maps new model to requested model from repo
            //updating dbcontext directly
            _mapper.Map(commandUpdateDto, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            //create object in database
            _repository.SaveChanges(); 

            //returning 204 No Content
            return NoContent();
        }

        //PATCH api/commands/{id}
        [HttpPatch("{id}")]
        [SwaggerOperation(Summary = "Partially updates a specific command by id number using the JSON 'replace' operation with the three attributes 'value' (new value), 'path' ('/line' or '/howTo'), and 'op' ('replace'). (Delete 'from'.)")]
        public ActionResult PartialCommandUpdate(int id, JsonPatchDocument<CommandUpdateDto> patchDoc)
        {
            //checking if requested model exists
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            //mapping model from repo to dto 
            var commandToPatch = _mapper.Map<CommandUpdateDto>(commandModelFromRepo);

            //applying patch (received patchDoc from client)
            patchDoc.ApplyTo(commandToPatch, ModelState);

            //validation
            if(!TryValidateModel(commandToPatch))
            {
                return ValidationProblem(ModelState);
            }

            //mapping the patched dto model to the repo model
            //updating dbcontext directly
            _mapper.Map(commandToPatch, commandModelFromRepo);

            _repository.UpdateCommand(commandModelFromRepo);

            _repository.SaveChanges();

            return NoContent();
        }

        //DELETE api/commands/{id}
        [HttpDelete("{id}")]
         [SwaggerOperation(Summary = "Deletes a specific command by id number.")]
        public ActionResult DeleteCommand(int id)
        {
            //checking if requested model exists
            var commandModelFromRepo = _repository.GetCommandById(id);
            if(commandModelFromRepo == null)
            {
                return NotFound();
            }

            //deleting command model 
            _repository.DeleteCommand(commandModelFromRepo);
            _repository.SaveChanges();

            return NoContent();
        }
        
    }
}