using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
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

      [HttpGet("{id}", Name = "AuthorById")]
      public IActionResult GetAuthorsById(Guid id)
      {
         try
         {
            var author = _repository.Author.GetAuthorById(id);

            if (author is null)
            {
               _logger.LogError($"Author with id: {id}, hasn't been found in db");
               return NotFound();
            }
            else
            {
               _logger.LogInfo($"Returned author for id: {id}");

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

      [HttpPost]
      public IActionResult CreateAuthor([FromBody] AuthorForCreateDto author) {

         try
         {
            if (author == null)
            {
               _logger.LogError("Author object sent from client is null.");
               return BadRequest("Author object is null");
            }

            if (!ModelState.IsValid)
            {
               _logger.LogError("Invalid author object sent from client.");
               return BadRequest("Invalid model object");
            }

            var authorEntity = _mapper.Map<Author>(author);

            _repository.Author.CreateAuthor(authorEntity);
            _repository.Save();

            var createdAuthor = _mapper.Map<AuthorDto>(author);

               return Ok(createdAuthor);
            //return CreatedAtRoute("AuthorById", new { id = createdAuthor.Id }, createdAuthor);

         }
         catch (Exception ex) {

            _logger.LogError($"Something went wrong inside CreateAuthor action: {ex.Message}");
            return StatusCode(500, "Internal server error");

         }
      
      
      }
   }
}
