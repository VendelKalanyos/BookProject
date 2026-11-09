using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookProject.Models;
using BookProject.Data;
using BookProject.Dtos.Author;

[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    private readonly BookDbContext _context;
    public AuthorsController(BookDbContext context)
    {
        _context = context;
    }

    // GET: api/Authorwoof
    [HttpGet]
    public async Task<ActionResult<IEnumerable<AuthorReadDto>>> GetAuthorwoof()
    {
        return await _context.Authors.Select(a =>
            new AuthorReadDto
            {
                Id = a.Id,
                Name = a.Name,
                DateOfBirth = a.DateOfBirth,
                PlaceOfBirth = a.PlaceOfBirth,
                BirthDateAndPlace = a.PlaceOfBirth + ", " +DateOnly.FromDateTime(a.DateOfBirth)
            }
            ).ToListAsync();
    }

    // GET: api/Authorwoof/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AuthorReadDto>> GetAuthorwoof(int id)
    {
        var authorwoof = await _context.Authors.FindAsync(id);

        if (authorwoof == null)
        {
            return NotFound();
        }

        var authorDto = new AuthorReadDto
        {
            Id = authorwoof.Id,
            Name = authorwoof.Name,
            DateOfBirth = authorwoof.DateOfBirth,
            PlaceOfBirth = authorwoof.PlaceOfBirth,
            BirthDateAndPlace = authorwoof.PlaceOfBirth +", "+authorwoof.DateOfBirth.ToString("yyyy.MM.dd")
        };

        return authorDto;
    }

    // PUT: api/Authorwoof/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAuthorwoof(int? id, Authorwoof authorwoof)
    {
        if (id != authorwoof.Id)
        {
            return BadRequest();
        }

        _context.Entry(authorwoof).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AuthorwoofExists(id))
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

    // POST: api/Authorwoof
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Authorwoof>> PostAuthorwoof(Authorwoof authorwoof)
    {
        _context.Authors.Add(authorwoof);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetAuthorwoof", new { id = authorwoof.Id }, authorwoof);
    }

    // DELETE: api/Authorwoof/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAuthorwoof(int? id)
    {
        var authorwoof = await _context.Authors.FindAsync(id);
        if (authorwoof == null)
        {
            return NotFound();
        }

        _context.Authors.Remove(authorwoof);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool AuthorwoofExists(int? id)
    {
        return _context.Authors.Any(e => e.Id == id);
    }
}
