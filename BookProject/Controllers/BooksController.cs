using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Data;

[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly BookDbContext _context;
    public BooksController(BookDbContext context)
    {
        _context = context;
    }

    // GET: api/Book miau
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Object>>> GetBook()
    {
        //return await _context.Books.ToListAsync();

        var books = await _context.Books.Select(x => new 
        {
            Id = x.Id,
            Title = x.Title,
            Category = x.Category,
            PublishedYear = x.PublishedYear,
            Pages = x.Pages,
            //Author = x.Author != null ? x.Author.Name : "Ismeretlen szerző"
            Author = x.Author.Name ?? "Ismeretlen szerző"
        }).ToListAsync();

        return books;
    }

    // GET: api/Book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Object>> GetBook(int id)
    {
        var book = await _context.Books.Include(x => x.Author).FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return new
        {
            Id = book.Id,
            Title = book.Title,
            Category = book.Category,
            PublishedYear = book.PublishedYear,
            Pages = book.Pages,
            //Author = book.Author != null ? x.Author.Name : "Ismeretlen szerző"
            Author = book.Author?.Name ?? "Ismeretlen szerző"
        };
    }

    // PUT: api/Book/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int? id, Book book)
    {
        if (id != book.Id)
        {
            return BadRequest();
        }

        _context.Entry(book).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BookExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // POST: api/Book
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Book>> PostBook(Book book)
    {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetBook", new { id = book.Id }, book);
    }

    // DELETE: api/Book/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int? id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool BookExists(int? id)
    {
        return _context.Books.Any(e => e.Id == id);
    }
}
