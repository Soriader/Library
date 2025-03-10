using Library.Models;

namespace Library;

public class BookService
{
    
    public BookService(int id, string title, string author, bool isAvailable)
    {
        var newBook = new Book();
        newBook.Id = id;
        newBook.Title = title;
        newBook.Author = author;
        newBook.IsAvailable = isAvailable;
    }
    
    public BookService AddBook(int id, string title, string author, bool isAvailable)
    {
        return new BookService(AddId(id), AddTitle(title), AddAuthor(author), IsAvailable(isAvailable));
    }

    private int AddId(int id)
    {
        return id;
    }
    private string AddTitle(string title)
    {
        title = GetUserInput("Please enter the name of the book you would like to add");
    
        if (ConfirmUserInput("This answer is correct? yes/no"))
        {
            return title;
        }
        else
        {
            title = GetUserInput("Please write correct title");
        
            if (ConfirmUserInput("This answer is correct? yes/no"))
            {
                return title;
            }
            else
            {
                Console.WriteLine("Please write correct title");
                return null;
            }
        }
    }

    private string AddAuthor(string author)
    {
        return author;
    }

    private bool IsAvailable(bool isAvailable)
    {
        return isAvailable;
    }
    
    private string GetUserInput(string prompt)
    {
        string userInput;
        do
        {
            Console.WriteLine(prompt);
            userInput = Console.ReadLine();
        } while (string.IsNullOrEmpty(userInput));

        return userInput;
    }

    private bool ConfirmUserInput(string prompt)
    {
        string userInput;
        do
        {
            Console.WriteLine(prompt);
            userInput = Console.ReadLine();
        } while (userInput != "yes" && userInput != "no");

        return userInput == "yes";
    }

    /*private static bool IsCorrect(string user) //not needed now but in the future it can be necessary
    {
        Console.WriteLine("Is your answer is correct? yes/no");
        var yesOrNo = Console.ReadLine() == "yes" ? true : false;

        if (yesOrNo)
        {
            return true;
        }
        
        return false;
    }*/
}