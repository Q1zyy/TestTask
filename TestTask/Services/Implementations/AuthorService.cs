using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class AuthorService : IAuthorService
    {

        private readonly ApplicationDbContext _context;

        public AuthorService(ApplicationDbContext applicationDbContext) 
        {
            _context = applicationDbContext;
        }

        public async Task<Author> GetAuthor()
        {
            var author = await _context.Authors
                .Select(a => new
                {
                    Author = a,
                    MaxLen = a.Books.Max(b => b.Title.Length)
                })
                .OrderByDescending(ab => ab.MaxLen)
                .ThenBy(ab => ab.Author.Id)
                .FirstOrDefaultAsync();
            return author?.Author;
        }

        public async Task<List<Author>> GetAuthors()
        {
            return await _context.Authors
                .Where(a => a.Books.Count(b => b.PublishDate.Year > 2015) % 2 == 0)
                .ToListAsync();
        }
    }
}
