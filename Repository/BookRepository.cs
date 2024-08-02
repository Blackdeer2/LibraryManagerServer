using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class BookRepository : RepositoryBase<Book>, IBookRepository
   {
      public BookRepository(RepositoryContext repositoryContext)
          : base(repositoryContext)
      {
      }

      public IEnumerable<Book> GetAllBooks()
      {
         return FindAll()
            .OrderBy(b => b.Title)
            .ToList();
      }

      public Book GetBookById(Guid id)
      {
         return FindByCondition(b => b.Id.Equals(id))
            .FirstOrDefault();
      }
   }
}
