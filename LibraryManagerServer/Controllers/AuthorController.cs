using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerServer.Controllers
{
   [Route("api/author")]
   [ApiController]
   public class AuthorController : ControllerBase
   {
      private ILoggerManager _logger;
      private IRepositoryWrapper _repository;
      private IMapper _mapper;

      public AuthorController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
      {
         _logger = logger;
         _repository = repository;
         _mapper = mapper;
      }

      [HttpGet("{id}/book")]
      public IActionResult GetAuthorsWithDetails(Guid id)
      {
         try
         {
            var author = _repository.Author.GetAuthorWithDetails(id);

            if (author == null)
            {
               _logger.LogError($"Author with id: {id}, hasn't been found in db");
               return NotFound();
            }
            else
            {
               _logger.LogInfo($"Returned author with details for id: {id}");

               var authorResult = _mapper.Map<AuthorDto>(author);
               return Ok(authorResult);
            }

         }
         catch (Exception ex)
         {

            _logger.LogError($"Something went wrong inside GetOwnerWithDetails action: {ex.Message}");
            return StatusCode(500, "Internal server error");
         }

      }
   }
}
