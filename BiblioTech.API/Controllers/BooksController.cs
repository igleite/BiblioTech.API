using BiblioTech.API.Entities;
using BiblioTech.API.MappingViewModels;
using BiblioTech.API.Models.InputModels;
using BiblioTech.API.Models.ViewModels;
using BiblioTech.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly BiblioTechDbContext _BiblioTechDbContext;

        public BooksController(BiblioTechDbContext BiblioTechDbContext)
        {
            _BiblioTechDbContext = BiblioTechDbContext;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<BookViewModel>>> GetAll()
        {
            var books = await _BiblioTechDbContext.Books.ToListAsync();

            if (books is null)
            {
                return BadRequest();
            }

            var booksViewModel = await LivroEmprestado(books.ConvertBookViewModel());

            return Ok(booksViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookViewModel>> GetById(int id)
        {

            var book = await _BiblioTechDbContext.Books.SingleOrDefaultAsync(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }
            
            var bookViewModel = await LivroEmprestado(book.ConvertBookViewModelById());

            return Ok(bookViewModel);
        }

        [HttpGet("GetByTitle/{title}")]
        public async Task<ActionResult<IList<BookViewModel>>> GetByTitle(string title)
        {

            var book = await _BiblioTechDbContext.Books.Where(b => b.Title.Contains(title)).ToListAsync();

            if (book is null)
            {
                return NotFound();
            }

            var bookViewModel = await LivroEmprestado(book.ConvertBookViewModel());

            return Ok(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(BookInputModel bookInputModel)
        {

            var book = new Book(bookInputModel.Title, bookInputModel.Author, bookInputModel.ISBN, bookInputModel.PublicationYear);

            await _BiblioTechDbContext.AddAsync(book);

            await _BiblioTechDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new {id = book.Id}, book);
        }

        [HttpPut()]
        public async Task<IActionResult> Update(BookInputModelUpdate bookUpdated)
        {

            var book = await _BiblioTechDbContext
                             .Books
                             .SingleOrDefaultAsync(b => b.Id == bookUpdated.Id);

            if (book is null)
            {
                return NotFound();
            }

            book.UpdateBook(bookUpdated.Title, bookUpdated.Author, bookUpdated.ISBN, bookUpdated.PublicationYear);

            await _BiblioTechDbContext.SaveChangesAsync();

            return Ok(book);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _BiblioTechDbContext.Books.SingleOrDefaultAsync(b => b.Id == id);

            if (book is null)
            {
                return NotFound();
            }

            _BiblioTechDbContext.Remove(book);

            await _BiblioTechDbContext.SaveChangesAsync();

            return Ok(book);
        }


        private async Task<IEnumerable<BookViewModel>> LivroEmprestado(IEnumerable<BookViewModel> booksViewModel)
        {
            foreach (var book in booksViewModel)
            {
                book.Emprestado = await _BiblioTechDbContext.BookLoans.AnyAsync(x => x.IdBook == book.Id);
            }

            return booksViewModel.OrderBy(x => x.Emprestado);
        }

        private async Task<BookViewModel> LivroEmprestado(BookViewModel book)
        {
            book.Emprestado = await _BiblioTechDbContext.BookLoans.AnyAsync(x => x.IdBook == book.Id);
            return book;
        }
    }
}