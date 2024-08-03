using Contracts;
using Entities;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
   public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
   {
      public AuthorRepository(RepositoryContext repositoryContext)
       : base(repositoryContext)
      {
      }

      public Author GetAuthorWithDetails(Guid authorId)
      {
         return FindByCondition(author => author.Id.Equals(authorId))
            .Include(b => b.Books)
            .FirstOrDefault();
      }

      public Author GetAuthorById(Guid authorId)
      {
         return FindByCondition(author => author.Id.Equals(authorId))
            .FirstOrDefault();

      }

      public void CreateAuthor(Author author)
      {
         Create(author);
      }

      public void UpdateAuthor(Author author)
      {
         Update(author);
      }

      public void DeleteAuthor(Author author)
      {
         Delete(author);
      }

   }
}
