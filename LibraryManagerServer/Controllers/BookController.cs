using AutoMapper;
using Contracts;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Security.Principal;
using System.Text;

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

      [HttpGet("title/{title}")]
      public IActionResult GetAllBookByTitle(string title) {

         try
         {
            var books = _repository.Book.BookByTitle(title);
            _logger.LogInfo("Return all books by title from database.");

            var booksResult = _mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(booksResult);

         }
         catch(Exception ex)
         {
            _logger.LogError($"Something went wrong inside GetAllBookByTitle action: {ex.Message}");
            return StatusCode(500, "Internal server error");
         }
      
      
      }

      [HttpGet("author/{authorId}")]
      public IActionResult GetAllBookByAuthor(Guid authorId)
      {

         try
         {
            var books = _repository.Book.BookByAuthor(authorId);
            _logger.LogInfo("Return all books by title from database.");

            var booksResult = _mapper.Map<IEnumerable<BookDto>>(books);

            return Ok(books);

         }
         catch (Exception ex)
         {
            _logger.LogError($"Something went wrong inside GetAllBookByTitle action: {ex.Message}");
            return StatusCode(500, "Internal server error");
         }


      }

      [HttpPost("author/{authorId}")]
      public IActionResult CreateBook(Guid authorId, [FromBody] BookForCreateDto book)
      {
         try
         {

            if (book == null)
            {
               _logger.LogError("Book object sent from client is null.");
               return BadRequest("Book object is null");
            }

            if (!ModelState.IsValid)
            {
               _logger.LogError("Invalid book object sent from client.");
               return BadRequest("Invalid model object");
            }

            var author = _repository.Author.GetAuthorById(authorId);

            if (author is null)
            {
               _logger.LogError($"Author with id: {authorId}, hasn't been found in db.");
               return NotFound();
            }

            var bookEntity = _mapper.Map<Book>(book);
            bookEntity.AuthorId = authorId;

            _repository.Book.CreateBook(bookEntity);
            _repository.Save();


            if (author.Books == null)
            {
               author.Books = new List<Book>();
            }

            author.Books.Add(bookEntity);

            _repository.Author.UpdateAuthor(author);
            _repository.Save();

            var authorResult = _mapper.Map<AuthorDto>(author);
            return Ok(authorResult);

            /* if (book == null)
             {
                _logger.LogError("Book object sent from client is null.");
                return BadRequest("Book object is null");
             }

             if (!ModelState.IsValid)
             {
                _logger.LogError("Invalid book object sent from client.");
                return BadRequest("Invalid model object");
             }

             var author = _repository.Author.GetAuthorById(authorId);
             var bookEntity = _mapper.Map<Book>(book);

             _repository.Book.CreateBook(bookEntity);

             author.Books.Add(bookEntity);

             _repository.Save();

             var createdBook = _mapper.Map<Book>(book);

             return Ok(createdBook);*/
         }
         catch (Exception ex)
         {

            _logger.LogError($"Something went wrong inside CreateBook action: {ex.Message}");
            return StatusCode(500, "Internal server error");

         }


      }

      [HttpPut("{id}")]
      public ActionResult UpdateBook(Guid id, [FromBody] BookForUpdateDto book)
      {

         try
         {
            if (book is null)
            {
               _logger.LogError("Book object sent from client is null.");
               return BadRequest("Book object is null");
            }
            if (!ModelState.IsValid)
            {
               _logger.LogError("Invalid Book object sent from client.");
               return BadRequest("Invalid model object");
            }

            var bookEntity = _repository.Book.GetBookById(id);

            if (bookEntity is null)
            {

               _logger.LogError($"Book with id: {id}, hasn't been found in db.");
               return NotFound();
            }


            _mapper.Map(book, bookEntity);
            _repository.Book.UpdateBook(bookEntity);
            _repository.Save();

            return NoContent();


         }
         catch (Exception ex)
         {

            _logger.LogError($"Something went wrong inside UpdateBook action: {ex.Message}");
            return StatusCode(500, "Internal server error");

         }

      }

      [HttpDelete("{id}")]
      public ActionResult DeleteBook(Guid id)
      {
         try
         {
            var book = _repository.Book.GetBookById(id);
            if(book is null)
            {
               _logger.LogError($"Book with id: {id}, hasn't been found in db.");
               return NotFound();
            }

            _repository.Book.DeleteBook(book);
            _repository.Save();

            return NoContent();

         }
         catch (Exception ex)
         {
            _logger.LogError($"Something went wrong inside DeleteBook action: {ex.Message}");
            return StatusCode(500, "Internal server error");
         }
      }
   }
}
