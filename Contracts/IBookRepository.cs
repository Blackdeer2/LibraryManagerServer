using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public interface IBookRepository: IRepositoryBase<Book>
   {
      IEnumerable<Book> GetAllBooks();
      IEnumerable<Book> BookByAuthor(Guid id);
      IEnumerable<Book> BookByTitle(string title);
      Book GetBookById(Guid id);
      void CreateBook(Book book);
      void UpdateBook(Book book);
      void DeleteBook(Book book);
   }
}
