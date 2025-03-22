using Library.Models;

namespace Library;

public class ReturnTheBook
{
    private readonly BookRepository _bookRepository;
    private readonly FindTheBook _findTheBook;
    private readonly IsAvailable _isAvailable;

    public ReturnTheBook()
    {
        _bookRepository = new BookRepository();
        _findTheBook = new FindTheBook();
        _isAvailable = new IsAvailable();
    }


    public string BookReturn()
    {
        Console.WriteLine("What book you want to return?");
        var book = _findTheBook.FindTheBookFromDatabase();

        if (book == null)
        {
            Console.WriteLine("This book does not exist");
            return "Error";
        }

        if (!BookIsOnTheLibrary(book))
        {
            Console.WriteLine("Book is available, you can't return to the library");
            return "Error";
        }

        BookBackToLibrary(book);
        
        Console.WriteLine("Book returned");
        return "Ok";

    }


    private bool BookBackToLibrary(Book book)
    {
        if (!_isAvailable.BookAvailable(book))
        {
            book.IsAvailable = true; 
            _bookRepository.Update(book);
            _bookRepository.Save();
            return true; 
        }

        return false;
    }
    
    
    private bool BookIsOnTheLibrary(Book book)
    {
        if (_isAvailable.BookAvailable(book))
        {
            return false;
        }
        
        return true;
    }
}