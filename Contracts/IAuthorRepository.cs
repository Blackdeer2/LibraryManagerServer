using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
   public interface IAuthorRepository : IRepositoryBase<Author>
   {
      Author GetAuthorWithDetails(Guid authorId);
      Author GetAuthorById(Guid id);
      void CreateAuthor(Author author);
      void UpdateAuthor(Author author);
      void DeleteAuthor(Author author);
   }
}
