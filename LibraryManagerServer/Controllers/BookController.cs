using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagerServer.Controllers
{
   [Route("api/book")]
   [ApiController]
   public class BookController : ControllerBase
   {
      private ILoggerManager _logger;
      private IRepositoryWrapper _repository;

      public BookController(ILoggerManager logger, IRepositoryWrapper repository)
      {
         _logger = logger;
         _repository = repository;
      }

      [HttpGet]
      public IActionResult GetAllBooks()
      {

         try
         {
            var books = _repository.Book.GetAllBooks();

            _logger.LogInfo("Return all books from database.");

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
