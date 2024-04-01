using BiblioTech.API.Entities;
using BiblioTech.API.Models.ViewModels;

namespace BiblioTech.API.MappingViewModels
{
    public static class MappingBooksViewModel
    {
        public static IEnumerable<BookViewModel> ConvertBookViewModel(
            this IEnumerable<Book> books)
        {
            return (from book in books
                    select new BookViewModel
                    {
                       Id = book.Id,
                       Author = book.Author,
                       ISBN = book.ISBN,
                       PublicationYear = book.PublicationYear,
                       Title = book.Title,

                    }).ToList();
        }

        public static BookViewModel ConvertBookViewModelById(
            this Book books)
        {
            return new BookViewModel

            {
                Id = books.Id,
                Author = books.Author,
                ISBN = books.ISBN,
                PublicationYear = books.PublicationYear,
                Title = books.Title,
            };
        }
    }
}
