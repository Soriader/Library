using Library.Models;

namespace Library;

public class BorrowTheBook
{
    private readonly BookRepository _bookRepository;
    private readonly FindTheBook _findTheBook;
    private readonly IsAvailable _isAvailable;
    
    public BorrowTheBook()
    {
        _bookRepository = new BookRepository();
        _findTheBook = new FindTheBook();
        _isAvailable = new IsAvailable();
    }
    
}