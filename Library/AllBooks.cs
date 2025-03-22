namespace Library;

public class AllBooks
{
    private readonly LibraryDbContext _libraryDbContext;

    public AllBooks()
    {
        _libraryDbContext = new LibraryDbContext();
    }

    public void PrintAllBooks()
    {
        var books = _libraryDbContext.Books.ToList();

        if (books.Any())
        {
            Console.WriteLine("List of books in the database:");
            
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.Id}, " +
                                  $"Title: {book.Title}, " +
                                  $"Author: {book.Author}, " +
                                  $"Available: {book.IsAvailable}, " +
                                  $"Category: {book.Category} " +
                                  $"ISBN: {book.ISBN}");
            }
        }
        else
        {
            Console.WriteLine("No books found in the database.");
        }
    }
}