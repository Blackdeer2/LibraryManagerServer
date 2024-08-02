using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class RepositoryWrapper : IRepositoryWrapper
   {
      private RepositoryContext _repoContext;
      private IAuthorRepository _author;
      private IBookRepository _book;

      public IAuthorRepository Author
      {
         get
         {
            if (_author == null)
            {
               _author = new AuthorRepository(_repoContext);
            }
            return _author;
         }
      }

      public IBookRepository Book
      {
         get
         {
            if (_book == null)
            {
               _book = new BookRepository(_repoContext);
            }
            return _book;
         }
      }

      public RepositoryWrapper(RepositoryContext repositoryContext)
      {
         _repoContext = repositoryContext;
      }
      public void Save()
      {
         _repoContext.SaveChanges();
      }
   }
}
