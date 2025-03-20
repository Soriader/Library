using Library.Models;
using Microsoft.EntityFrameworkCore;

namespace Library;

public class BookRepository : IRepository<Book>
{
    private readonly LibraryDbContext _context = null;
    private DbSet<Book> Books = null;
    
    public BookRepository()
    {
        _context = new LibraryDbContext();
        Books = _context.Set<Book>();
    }

    public void Insert(Book entity)
    {
        Books.Add(entity);
    }

    public Book GetByTitle(string title)
    {
        return Books.FirstOrDefault(b => b.Title.ToLower() == title.ToLower());
    }

    public Book GetByAuthor(string author)
    {
        return Books.FirstOrDefault(b => b.Author.ToLower() == author.ToLower());
    }

    public Book GetById(int entityId)
    {
        return Books.Find(entityId);
    }

    public IEnumerable<Book> GetAll()
    {
        return Books.ToList();
    }

    public void Update(Book entity)
    {
        Books.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
    }

    public void Delete(int entityId)
    {
        Books.Remove(GetById(entityId));
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}