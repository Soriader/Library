using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library;

public class RemoveBook
{
    private readonly FindTheBook _findTheBook;
    private readonly BookRepository _bookRepository;

    public RemoveBook()
    {
        _findTheBook = new FindTheBook();
        _bookRepository = new BookRepository();
    }

    public string DeleteBook()
    {
        var bookForDelete = _findTheBook.FindTheBookFromDatabase();

        if (bookForDelete != null)
        {
            _bookRepository.Delete(bookForDelete.Id);
            return $"Book '{bookForDelete.Title}' by {bookForDelete.Author} has been removed.";
        }

        return "Book not found.";
    }
}