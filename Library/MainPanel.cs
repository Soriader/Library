using Library.Models;

namespace Library;

public class MainPanel
{
    private readonly AddNewBook _addNewBook;
    private readonly FindTheBook _findTheBook;
    private readonly EditTheExistBooks _editTheExistBooks;
    private readonly BookRepository _bookRepository;
    private readonly RemoveBook _removeBook;
    private readonly BorrowTheBook _borrowTheBook;
    private readonly ReturnTheBook _returnTheBook;
    private readonly UIService _uiService;
    
    public MainPanel()
    {
        _addNewBook = new AddNewBook();
        _findTheBook = new FindTheBook();
        _editTheExistBooks = new EditTheExistBooks();
        _bookRepository = new BookRepository();
        _removeBook = new RemoveBook();
        _borrowTheBook = new BorrowTheBook();
        _returnTheBook = new ReturnTheBook();
        _uiService = new UIService();
    }
    
    public void Menu()
    {
        Console.WriteLine("Please enter the name of the category you would like to run:"
                          + "\n1.Add new book"
                          + "\n2.Find the book"
                          + "\n3.Change the properties of the selected book"
                          + "\n4.Delete of the selected book"
                          + "\n5.Borrow the selected book"
                          + "\n6.Return the selected book"
                          + "\n7.Show all books");

        var answer = Console.ReadLine();

        if (int.TryParse(answer, out int option) && (option == 1 
                                                     || option == 2 
                                                     || option == 3 
                                                     || option == 4
                                                     || option == 5 
                                                     || option == 6 
                                                     || option == 7))
        {
            switch (option)
            {
                case 1:
                {
                    var book = new Book(_uiService.GetTitle(), 
                        _uiService.GetAuthor(), 
                        _uiService.GetCategory(),
                        _uiService.GetISBN());
                    
                    book.IsAvailable = true;
                    _bookRepository.Insert(book);
                    _bookRepository.Save();
                    Continue();
                    break;
                }

                case 2:
                {
                    _findTheBook.BookFinder();
                    Continue();
                    break;
                }

                case 3:
                {
                    _editTheExistBooks.BookEditor();
                    Continue();
                    break;
                }
                
                case 4:
                {
                    _removeBook.DeleteBook();
                    Continue();
                    break;
                }

                case 5:
                {
                    _borrowTheBook.Borrow();
                    Continue();
                    break;
                }
                
                case 6:
                {
                    _returnTheBook.BookReturn();
                    Continue();
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid option selected.");
            Menu();
        }
    }

    private void Continue()
    {
        Console.WriteLine("You want to do something else or exit?"
                          + "\n1.Back to Main Menu"
                          + "\n2.Exit");

        var answer = Console.ReadLine();

        if (!int.TryParse(answer, out int option) || (option != 1 && option != 2))
        {
            while (true)
            {
                Console.WriteLine("Please write correct number!");
                answer = Console.ReadLine();

                if (int.TryParse(answer, out option) && (option == 1 || option == 2))
                {
                    break;
                }
            }
        }

        switch (option)
        {
            case 1:
            {
                Menu();
                break;
            }
            case 2:
            {
                Console.WriteLine("Thanks for visiting!");
                break;
            }
        }
    }
}