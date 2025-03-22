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


    public void Borrow()
    {
        Console.WriteLine("What book you want to borrow?");
        
        var book = _findTheBook.FindTheBookFromDatabase();

        if (book != null)
        {
            BookIsAvailable(book);
        }
        else
        {
            Console.WriteLine("Book is not available");
        }
    }

    private bool BookIsAvailable(Book book)
    {
        if (_isAvailable.BookAvailable(book))
        {
            Console.WriteLine("You can borrow!");
            book.IsAvailable = false; 
            _bookRepository.Update(book);
            _bookRepository.Save();
            return true; 
        }

        return false;
    }
}