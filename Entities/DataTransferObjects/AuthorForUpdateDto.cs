﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DataTransferObjects
{
   public class AuthorForUpdateDto
   {
      [Required(ErrorMessage = "Name is required")]
      [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
      public string? Name { get; set; }
   }
}
