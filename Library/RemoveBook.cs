using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library;

public class RemoveBook
{
    private FindTheBook findTheBook;

    public RemoveBook()
    {
        findTheBook = new FindTheBook();
    }

    public string DeleteBook()
    {
        var bookForDelete = findTheBook.FindTheBookFromDatabase();

        if (bookForDelete != null)
        {
            if (DeleteBookFromDatabase(bookForDelete.Id))
            {
                return $"Book '{bookForDelete.Title}' by {bookForDelete.Author} has been removed.";
            }
            else
            {
                return "Failed to remove the book.";
            }
        }

        return "Book not found.";
    }

    private bool DeleteBookFromDatabase(int bookId)
    {
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=LibraryApp;Integrated Security=True;TrustServerCertificate=true;";
        string query = "DELETE FROM Books WHERE Id = @BookId";

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            try
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BookId", bookId);

                    int rowsAffected = command.ExecuteNonQuery();

                    return rowsAffected > 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error deleting book: " + ex.Message);
                return false;
            }
        }
    }
}