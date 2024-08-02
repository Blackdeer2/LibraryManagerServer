using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
   [Table("book")]
   public class Book
   {
      public Guid BookId { get; set; }

      [Required(ErrorMessage = "Title type is required")]
      public string? Title { get; set; }

      [ForeignKey(nameof(Author))]
      public Guid AuthorId { get; set; }
      public Author? Author { get; set; }
   }
}
