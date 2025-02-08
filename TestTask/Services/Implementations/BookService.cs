using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class BookService : IBookService
    {

        private readonly ApplicationDbContext _context;

        public BookService(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public async Task<Book> GetBook()
        {
            var max = await _context.Books.MaxAsync(b => b.QuantityPublished * b.Price);
            return await _context.Books
                .FirstOrDefaultAsync(b => b.QuantityPublished * b.Price == max);
        }

        public async Task<List<Book>> GetBooks()
        {
            var date = new DateTime(2012, 5, 25);
            return await _context.Books
                .Where(b => b.Title.Contains("Red") && b.PublishDate > date)
                .ToListAsync();
        }
    }
}
