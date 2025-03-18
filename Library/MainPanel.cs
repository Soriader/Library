using Library.Models;

namespace Library;

public class MainPanel
{
    public void Menu()
    {
        Console.WriteLine("Please enter the name of the category you would like to run:"
                          + "\n1.Add new book" 
                          + "\n2.Find the book");
        
        var answer = Console.ReadLine();
        
        if (int.TryParse(answer, out int option) && (option == 1 || option == 2))
        {
            switch (option)
            {
                case 1:
                {
                    var bookService = new AddNewBook();
                    var bookRepository = new BookRepository();
                    var book = new Book(bookService.GetTitle(), bookService.GetAuthor(), bookService.GetCategory());
                    book.IsAvailable = true;
                    bookRepository.Insert(book);
                    bookRepository.Save();
                    Continue();
                    break;
                    
                }

                case 2:
                {
                    var findTheBook = new FindTheBook();
                    findTheBook.BookFinder();
                    Continue();
                    break;
                }
            }

        }

    }


    private void Continue()
    {
        Console.WriteLine("You want to do something else or exit?"
                          + "\n1.Back to Main Menu"
                          + "\n2.Exit");
            
        var answer = Console.ReadLine();

        if (!int.TryParse(answer, out int option) || (option != 1 || option != 2))
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