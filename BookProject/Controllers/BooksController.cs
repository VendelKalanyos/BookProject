using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Data;
using BookProject.Dtos.Book;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Mvc.Routing;

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
    public async Task<ActionResult<IEnumerable<BookReadDto>>> GetBook()
    {
        //return await _context.Books.ToListAsync();

        var books = await _context.Books.Select(x => new BookReadDto
        {
            Id = x.Id,
            Title = x.Title,
            Category = x.Category,
            PublishedYear = x.PublishedYear,
            Pages = x.Pages,
            //Author = x.Author != null ? x.Author.Name : "Ismeretlen szerző"
            Author = x.Author.Name ?? "Ismeretlen szerző",
            AuthorId = x.AuthorId,
        }).ToListAsync();

        return books;
    }

    // GET: api/Book/5
    [HttpGet("{id}")]
    public async Task<ActionResult<BookReadDto>> GetBook(int id)
    {
        var book = await _context.Books.Include(x => x.Author).FirstOrDefaultAsync(b => b.Id == id);

        if (book == null)
        {
            return NotFound();
        }

        return new BookReadDto
        {
            Id = book.Id,
            Title = book.Title,
            Category = book.Category,
            PublishedYear = book.PublishedYear,
            Pages = book.Pages,
            //Author = book.Author != null ? x.Author.Name : "Ismeretlen szerző"
            Author = book.Author?.Name ?? "Ismeretlen szerző",
            AuthorId = book.AuthorId 
        };
    }

    // PUT: api/Book/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutBook(int? id, BookUpdateDto bookDto)
    {

        if (id != bookDto.Id)
        {
            return BadRequest();
        }

        var book = await _context.Books.FindAsync(id);
        var author = await _context.Authors.FindAsync(bookDto.AuthorId);

        if (book == null) return NotFound("A könyv nem létezik!");
        if (author == null) return NotFound("A szerő nem létezik!");

        book.AuthorId = bookDto.AuthorId;
        book.Title = bookDto.Title;
        book.Available = bookDto.Available;
        book.Category = bookDto.Category;
        book.PublishedYear = bookDto.PublishedYear;
        book.Pages = bookDto.Pages;

        //_context.Entry(book).State = EntityState.Modified;

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
    public async Task<ActionResult<BookReadDto>> PostBook(BookCreateDto bookDto)
    {
        var author = await _context.Authors.FindAsync(bookDto.AuthorId);

        if (author == null) { return NotFound("Nincs ilyen szerző(=csengőfrász elvitték, imperialista ügynök volt grrr)"); }

        var book = new Book
        {
            Title = bookDto.Title,
            Category = bookDto.Category,
            AuthorId = bookDto.AuthorId,
            Pages = bookDto.Pages,
            PublishedYear = bookDto.PublishedYear,
            Active = true
        };

        _context.Books.Add(book);
        await _context.SaveChangesAsync();

        var newbook = new BookReadDto
        {
            Id = book.Id,
            Title = book.Title,
            Pages = book.Pages,
            PublishedYear = book.PublishedYear,
            Category = book.Category,
            AuthorId = book.AuthorId,
            Author = author.Name

        };

        return CreatedAtAction("GetBook", new { id = book.Id }, newbook);
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
