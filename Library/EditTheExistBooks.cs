using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library;

public class EditTheExistBooks
{
     private string connectionString =
        "Server=localhost\\SQLEXPRESS;Database=LibraryApp;Integrated Security=True;TrustServerCertificate=true;";

    private FindTheBook findTheBook;
    private AddNewBook addNewBookMethod;

    public EditTheExistBooks()
    {
        findTheBook = new FindTheBook();
        addNewBookMethod = new AddNewBook();
    }

    public void BookEditor()
    {
        try
        {
            var bookToFind = findTheBook.FindTheBookFromDatabase();

            if (bookToFind != null)
            {
                Console.WriteLine($"Book found: {bookToFind.Title} by {bookToFind.Author}");

                Console.WriteLine("Which part of the book do you want to edit?"
                                  + "\n1. Title"
                                  + "\n2. Author"
                                  + "\n3. Category");

                int option;
                while (true)
                {
                    var answer = Console.ReadLine();
                    if (int.TryParse(answer, out option) && (option == 1 || option == 2 || option == 3))
                    {
                        break;
                    }
                    Console.WriteLine("Please enter a valid number (1, 2, or 3)!");
                }

                Console.WriteLine("Enter the new value:");

                switch (option)
                {
                    case 1:
                        bookToFind.Title = addNewBookMethod.GetTitle();
                        break;
                    case 2:
                        bookToFind.Author = addNewBookMethod.GetAuthor();
                        break;
                    case 3:
                        bookToFind.Category = addNewBookMethod.GetCategory();
                        break;
                }

                UpdateBookInDatabase(bookToFind);
                Console.WriteLine("Book updated successfully!");
            }
            else
            {
                Console.WriteLine("Book not found.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
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
    }
}