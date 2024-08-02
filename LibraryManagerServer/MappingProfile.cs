﻿using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagerServer
{
   public class MappingProfile : Profile
   {
        public MappingProfile()
        {
         CreateMap<Book, BookDto>();
         CreateMap<Author, AuthorDto>();
        }
    }
}
