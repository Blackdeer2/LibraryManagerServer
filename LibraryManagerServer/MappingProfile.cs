using AutoMapper;
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
         CreateMap<AuthorForCreateDto, Author>();
         CreateMap<BookForCreateDto, Book>();
         CreateMap<BookForUpdateDto, Book>();
         CreateMap<BookExportDto, Book>();

      }
    }
}
