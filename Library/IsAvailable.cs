using Library.Models;

namespace Library;

public class IsAvailable
{
    private readonly BookRepository _bookRepository;
    private readonly FindTheBook _findTheBook;

    public IsAvailable()
    {
        _bookRepository = new BookRepository();
        _findTheBook = new FindTheBook();
    }

    public bool BookAvailable(Book book)
    {
        if (book.IsAvailable)
        {
            Console.WriteLine($"{book.Title} {book.Author} is available");
            return true;
        }
        
        return false;
    }
}