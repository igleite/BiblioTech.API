﻿using BiblioTech.API.Entities;
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

            var booksViewModel = books.ConvertBookViewModel();

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
            
            var bookViewModel = book.ConvertBookViewModelById();

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, BookInputModel bookUpdated)
        {

            var book = await _BiblioTechDbContext
                             .Books
                             .SingleOrDefaultAsync(b => b.Id == id);

            if (book is null)
            {
                return BadRequest();
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

    }
}
