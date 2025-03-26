using Library.Models;

namespace Library;

public class UIService
{
    
    public string BookReturn()
    {
        Console.WriteLine("What book you want to return?");
        var book = _findTheBook.FindTheBookFromDatabase();

        if (book == null)
        {
            Console.WriteLine("This book does not exist");
            return "Error";
        }

        if (!BookIsOnTheLibrary(book))
        {
            Console.WriteLine("Book is available, you can't return to the library");
            return "Error";
        }

        BookBackToLibrary(book);
        
        Console.WriteLine("Book returned");
        return "Ok";

    }


    private bool BookBackToLibrary(Book book)
    {
        if (!_isAvailable.BookAvailable(book))
        {
            book.IsAvailable = true; 
            _bookRepository.Update(book);
            _bookRepository.Save();
            return true; 
        }

        return false;
    }
    
    
    private bool BookIsOnTheLibrary(Book book)
    {
        if (_isAvailable.BookAvailable(book))
        {
            return false;
        }
        
        return true;
    }
    public string DeleteBook()
    {
        var bookForDelete = _findTheBook.FindTheBookFromDatabase();

        if (bookForDelete != null)
        {
            _bookRepository.Delete(bookForDelete.Id);
            _bookRepository.Save();
            return $"Book '{bookForDelete.Title}' by {bookForDelete.Author} has been removed.";
        }

        return "Book not found.";
    }
    
    public string BookFinder()
    {
        Book foundBook = FindTheBookFromDatabase();

        if (foundBook != null)
        {
            return $"Book found: {foundBook.Title} by {foundBook.Author}";
        }
        
        Console.WriteLine("Book not found in the database.");
        return "Book not found";
    }

    public Book FindTheBookFromDatabase()
    {
        Console.WriteLine("You want to find the book by:"
                          + "\n1. Title"
                          + "\n2. Author");

        var answer = Console.ReadLine();

        if (int.TryParse(answer, out int error) && error != 1 && error != 2)
        {
            Console.WriteLine("Invalid option chosen.");
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
                        bookToFind.Title = _uiService.GetTitle();
                        break;
                    case 2:
                        bookToFind.Author = _uiService.GetAuthor();
                        break;
                    case 3:
                        bookToFind.Category = _uiService.GetCategory();
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
    
    public void Borrow()
    {
        Console.WriteLine("What book you want to borrow?");
        
        var book = _findTheBook.FindTheBookFromDatabase();

        if (book != null)
        {
            IsBookAvailable(book);
        }
    }
    
    public bool IsBookAvailable(Book book)
    {
        if (book.IsAvailable)
        {
            Console.WriteLine($"{book.Title} {book.Author} is available" +
                              $"You can borrow!");
            return true;
        }
        
        Console.WriteLine("Book is not available, you cant't borrow");
        return false;
    }
    
    public string GetTitle()
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
    
    public string GetAuthor()
    {
        var author = GetUserInput("Please enter the name of the author you would like to add");

        if (ConfirmUserInput("This answer is correct? yes/no"))
        {
            return author;
        }
        else
        {
            author = GetUserInput("Please write correct author");

            if (ConfirmUserInput("This answer is correct? yes/no"))
            {
                return author;
            }
            else
            {
                while (true)
                {
                    author = GetUserInput("Please write correct author!");
                    
                    if (ConfirmUserInput("This answer is correct? yes/no"))
                    {
                        break;
                    }
                }
                return author;
            }
        }
    }

    public string GetCategory()
    {
        var category = GetUserInputForInt("Please enter the number of the category of this book. We have five category" 
                                          + "\n1. Fantasy"
                                          + "\n2. Sci-Fi"
                                          + "\n3. Thriller/Crime"
                                          + "\n4. Romance"
                                          + "\n5. Young Adult", 1, 5);

        if (ConfirmUserInput("This answer is correct? yes/no"))
        {
            string answer = "";
            switch (category)
            {
                case "1":
                {
                    category = "Fantasy";
                    break;
                }
                case "2":
                {
                    category = "Sci-Fi";
                    break;
                }
                case "3":
                {
                    category = "Thriller";
                    break;
                }
                case "4":
                {
                    category = "Romance";
                    break;
                }
                case "5":
                {
                    category = "Young Adult";
                    break;
                }
            }
            
            return category;
        }
        else
        {
            category = GetUserInputForInt("Please write correct category", 1, 5);

            if (ConfirmUserInput("This answer is correct? yes/no"))
            {
                return category;
            }
            else
            {
                while (true)
                {
                    category = GetUserInputForInt("Please write correct category!", 1, 5);
                    
                    if (ConfirmUserInput("This answer is correct? yes/no"))
                    {
                        break;
                    }
                }
                return category;
            }
        }
    }

    public long GetISBN()
    {
        long code = CorrectISBN("Please enter the ISBN of the book you would like to add" +
                               "\nThis code needs 13 numbers:");
        return code;
    }

    private long CorrectISBN(string prompt)
    {
        long code = 0;
        bool isValid = false;

        while (!isValid)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            if (input.Length == 13 && input.All(char.IsDigit))
            {
                if (long.TryParse(input, out code))
                {
                    isValid = true;
                }
                else
                {
                    Console.WriteLine("Invalid ISBN code. It must be a valid 13-digit number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ISBN code. It must be exactly 13 digits long and contain only numbers.");
            }
        }

        return code;
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
    
    private string GetUserInputWithConfirmation(string user)
    {
        string userInput;
        do
        {
            Console.WriteLine(user);
            userInput = Console.ReadLine().Trim();
        } while (string.IsNullOrEmpty(userInput) || !ConfirmUserInput($"Is '{userInput}' correct? (yes/no)"));

        return userInput;
    }
    
    private string GetUserInputForInt(string prompt, int min, int max)
    {
        string userInput;
        int number;
        bool isValid;

        do
        {
            Console.WriteLine(prompt); 
            userInput = Console.ReadLine();

            bool isNumber = int.TryParse(userInput, out number);

            isValid = isNumber && number >= min && number <= max;

            if (!isValid)
            {
                Console.WriteLine($"Error: Enter a number from {min} to {max}.");
            }

        } while (!isValid);

        return userInput;
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
    
}