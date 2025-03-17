using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library;

public class EditTheExistBooks
{
    private string connectionString =
        "Server=localhost\\SQLEXPRESS;Database=LibraryApp;Integrated Security=True;TrustServerCertificate=true;";
    
    private FindTheBook FindTheBook;
    public Book BookEditor()
    {
        FindTheBook.BookFinder();
        
        if (FindTheBook.BookFinder() != null)
        {
            Console.WriteLine("Ok write the part of the book you want to edit" 
                              +"\n1. Name of book" 
                              +"\n2. Author" 
                              +"\n3. Category");
            
            var answer = Console.ReadLine();
            
            if (!int.TryParse(answer, out int option) || (option != 1 || option != 2 || option != 3))
            {
                while (true)
                {
                    Console.WriteLine("Please write correct number!");
                    answer = Console.ReadLine();

                    if (int.TryParse(answer, out option) && (option == 1 || option == 2 || option == 3))
                    {
                        break;
                    }
                }
            }
            
        }
        
        Console.WriteLine("Book not found");
        return null;
    }
    
    private void UpdateBookInDatabase(Book book)
    {
        string query = "UPDATE Books SET Title = @Title, Author = @Author, Category = @Category WHERE Id = @Id";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", book.Title);
            command.Parameters.AddWithValue("@Author", book.Author);
            command.Parameters.AddWithValue("@Category", book.Category);
            command.Parameters.AddWithValue("@Id", book.Id);

            connection.Open();
            command.ExecuteNonQuery();
        }

        Console.WriteLine("Book updated successfully.");
    }
    
    /*private bool BookExists(string answer) //Now I don't need it but in the future it's kind of helpful
    {
        if (FindTheBook.BookFinder() != null)
        {
            return true;
        }
        
        return false;
    }*/
    
}