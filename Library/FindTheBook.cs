using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library;

public class FindTheBook
{
    private string connectionString =
        "Server=localhost\\SQLEXPRESS;Database=LibraryApp;Integrated Security=True;TrustServerCertificate=true;";

    public string BookFinder()
    {
        Book foundBook = FindTheBookFromDatabase();

        if (foundBook != null)
        {
            Console.WriteLine($"Book found: {foundBook.Title} by {foundBook.Author}");
        }

        return "Book not found.";

    }
    
    public Book FindTheBookFromDatabase()
    {
        Console.WriteLine("You want to find the book by:" 
                          + "\n1. Name of book" 
                          + "\n2. Name of author");
        
        var answer = Console.ReadLine();

        if (int.TryParse(answer, out int option) && (option == 1 || option == 2))
        {
            string searchTerm = GetUserInput(option == 1 ? "Please enter the book title:" : "Please enter the author name:");
            return FindTheBookBy(searchTerm, option);
        }
        else
        {
            Console.WriteLine("Invalid option selected.");
            return null;
        }
    }

    private Book FindTheBookBy(string searchTerm, int option)
    {
        string query = option == 1 
            ? "SELECT * FROM Books WHERE Title = @SearchTerm" 
            : "SELECT * FROM Books WHERE Author = @SearchTerm";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@SearchTerm", searchTerm);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                return new Book(
                    reader["Title"].ToString(),
                    reader["Author"].ToString(),
                    reader["Category"].ToString()
                )
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    IsAvailable = Convert.ToBoolean(reader["IsAvailable"])
                };
            }
        }

        return null;
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