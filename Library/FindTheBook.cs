using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library;

public class FindTheBook
{
    public FindTheBook(Book bookWhatINeed)
    {
        Book book = bookWhatINeed;
    }


    public string CorrectTheTitle()
    {
        Console.WriteLine("Ok you choose to find the book by title, please enter the title");
        var title = Console.ReadLine().Trim();

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
                while (true)
                {
                    title = GetUserInput("Please write correct title!");
                    
                    if (ConfirmUserInput("This answer is correct? yes/no"))
                    {
                        break;
                    }
                }
                return title;
            }
        }
        
    }
    
    public string CorrectTheAuthor()
    {
        Console.WriteLine("Ok you choose to find the book by author, please enter the author");
        var author = Console.ReadLine().Trim();

        if (ConfirmUserInput("This answer is correct? yes/no"))
        {
            return author;
        }
        else
        {
            author = GetUserInput("Please write correct title");

            if (ConfirmUserInput("This answer is correct? yes/no"))
            {
                return author;
            }
            else
            {
                while (true)
                {
                    author = GetUserInput("Please write correct title!");
                    
                    if (ConfirmUserInput("This answer is correct? yes/no"))
                    {
                        break;
                    }
                }
                return author;
            }
        }
    }
    private bool ConfirmUserInput(string user)
    {
        string userInput;
        do
        {
            Console.WriteLine(user);
            userInput = Console.ReadLine().ToLower();
        } while (userInput != "yes" && userInput != "no");

        return userInput == "yes";
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
}