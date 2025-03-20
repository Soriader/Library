using Library.Models;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic;

namespace Library;

public class FindTheBook
{
    private readonly BookRepository _bookRepository;

    public FindTheBook()
    {
        _bookRepository = new BookRepository();
    }

    public string BookFinder()
    {
        Book foundBook = FindTheBookFromDatabase();

        if (foundBook != null)
        {
            return $"Book found: {foundBook.Title} by {foundBook.Author}";
        }

        Console.WriteLine("Book not found");
        return null;
        
    }

    public Book FindTheBookFromDatabase()
    {
        Console.WriteLine("You want to find the book by:"
                          + "\n1. Title"
                          + "\n2. Author");

        var answer = Console.ReadLine();

        if (int.TryParse(answer, out int error) && error != 1 && error != 2)
        {
            Console.WriteLine("Invalid option choosen.");
            return null;
        }
        
        bool searchOption = (int.Parse(answer) - 1) == 0;
            
        string searchTerm =
            GetUserInput(searchOption ? "Please enter the book title:" : "Please enter the author:");
        
        return ReturnBook(searchOption, searchTerm);
    }

    private Book ReturnBook(bool searchOption, string searchTerm)
    {
        if (searchOption)
        {
            return _bookRepository.GetByTitle(searchTerm);
        }

        return _bookRepository.GetByAuthor(searchTerm);
    }

    private string GetUserInput(string prompt)
    {
        string userInput;
        do
        {
            Console.WriteLine(prompt);
            userInput = Console.ReadLine().Trim();
        } while (string.IsNullOrEmpty(userInput) || !ConfirmUserInput($"Is '{userInput}' correct? (yes/no)"));

        return userInput;
    }

    private bool ConfirmUserInput(string message)
    {
        string userInput;
        do
        {
            Console.WriteLine(message);
            userInput = Console.ReadLine().ToLower();
        } while (userInput != "yes" && userInput != "no");

        return userInput == "yes";
    }
}