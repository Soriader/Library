using Library.Models;

namespace Library;

public class BookService
{
    private readonly BookRepository _bookRepository;
    public BookService()
    {
        _bookRepository = new BookRepository();
    }
    private string GetTitle()
    {
        var title = GetUserInput("Please enter the name of the book you would like to add");
    
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

    private string GetAuthor()
    {
        return "";
    }
    
    
    private string GetUserInput(string user)
    {
        string userInput;
        do
        {
            Console.WriteLine(user);
            userInput = Console.ReadLine();
        } while (string.IsNullOrEmpty(userInput));

        return userInput;
    }

    private bool ConfirmUserInput(string user)
    {
        string userInput;
        do
        {
            Console.WriteLine(user);
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