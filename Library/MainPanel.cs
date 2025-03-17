using Library.Models;

namespace Library;

public class MainPanel
{
    public void Menu()
    {
        Console.WriteLine("Welcome to the Library!"
                          + "\nPlease enter the name of the category you would like to run:"
                          + "\n1.Add new book" 
                          + "\n2.Find the book");
        
        var answer = Console.ReadLine();
        
        if (int.TryParse(answer, out int option) && (option == 1 || option == 2))
        {
            switch (option)
            {
                case 1:
                {
                    var bookService = new BookService();
                    var bookRepository = new BookRepository();
                    var book = new Book(bookService.GetTitle(), bookService.GetAuthor(), bookService.GetCategory());
                    book.IsAvailable = true;
                    bookRepository.Insert(book);
                    bookRepository.Save();
                    break;
                }

                case 2:
                {
                    var findTheBook = new FindTheBook();
                    findTheBook.BookFinder();
                    break;
                }
            }

        }

    }


    private void Continue()
    {
        
    }
}