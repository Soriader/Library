using Library.Models;
using Microsoft.Data.SqlClient;

namespace Library;

public class EditTheExistBooks
{

    private readonly FindTheBook _findTheBook;
    private AddNewBook changeTheBookPropertise;
    private readonly BookRepository _bookRepository;

    public EditTheExistBooks()
    {
        _findTheBook = new FindTheBook();
        _bookRepository = new BookRepository();
        
    }

    public void BookEditor()
    {
        try
        {
            var bookToFind = _findTheBook.FindTheBookFromDatabase();

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
                        bookToFind.Title = changeTheBookPropertise.GetTitle();
                        break;
                    case 2:
                        bookToFind.Author = changeTheBookPropertise.GetAuthor();
                        break;
                    case 3:
                        bookToFind.Category = changeTheBookPropertise.GetCategory();
                        break;
                }

                _bookRepository.Update(bookToFind);
                _bookRepository.Save();
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
    
}