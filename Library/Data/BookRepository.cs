using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library;

public class BookRepository : IRepository<Book>
{
    private readonly LibraryDbContext _context;

    public void Insert(Book entity)
    {
        throw new NotImplementedException();
    }

    public Book GetById(int entityId)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Book> GetAll()
    {
        throw new NotImplementedException();
    }

    public void Update(Book entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int entityId)
    {
        throw new NotImplementedException();
    }

    public void Save()
    {
        throw new NotImplementedException();
    }
}