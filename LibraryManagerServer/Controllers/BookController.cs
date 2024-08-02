using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace LibraryManagerServer.Controllers
{
   [Route("api/book")]
   [ApiController]
   public class BookController : ControllerBase
   {
      private ILoggerManager _logger;
      private IRepositoryWrapper _repository;
      private IMapper _mapper;

      public BookController(ILoggerManager logger, IRepositoryWrapper repository, IMapper mapper)
      {
         _logger = logger;
         _repository = repository;
         _mapper = mapper;
      }

      [HttpGet("{id}", Name = "BookById")]
      public IActionResult GetBookById(Guid id)
      {
         try
         {
            var book = _repository.Book.GetBookById(id);
            if (book is null)
            {
               _logger.LogError($"Book with id: {id}, hasn't been found in db.");
               return NotFound();
            }
            else
            {
               _logger.LogInfo($"Returned owner with id: {id}");
               var bookResult = _mapper.Map<BookDto>(book);
               return Ok(bookResult);
            }
         }
         catch (Exception ex)
         {

            _logger.LogError($"Something went wrong inside GetOwnerById action: {ex.Message}");
            return StatusCode(500, "Internal server error");

         }
      }


      [HttpGet]
      public IActionResult GetAllBooks()
      {

         try
         {
            var books = _repository.Book.GetAllBooks();

            _logger.LogInfo("Return all books from database.");

            var booksResult = _mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(books);
         }
         catch (Exception ex)
         {
            _logger.LogError($"Something went wrong inside GetAllBooks action: {ex.Message}");
            return StatusCode(500, "Internal server error");
         }


      }
   }
}
