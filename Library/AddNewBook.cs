using Library.Models;

namespace Library;

public class AddNewBook
{
    private readonly BookRepository _bookRepository;
    private readonly UIService _uiService;
    public AddNewBook()
    {
        _bookRepository = new BookRepository();
        _uiService = new UIService();
    }
    
}