using Library.Models;

namespace Library;

public class UIService
{
    private readonly BookRepository _bookRepository;
    
    public UIService()
    {
        _bookRepository = new BookRepository();
    }
    
    public void BookReturn()
    {
        Console.WriteLine("What book you want to return?");
        var book = FindTheBookFromDatabase();

        if (book == null)
        {
            Console.WriteLine("This book does not exist");
            return;
        }

        if (!BookIsOnTheLibrary(book))
        {
            Console.WriteLine("Book is available, you can't return to the library");
            return;
        }

        BookBackToLibrary(book);
        
        Console.WriteLine("Book returned");
    }
    
    private bool BookBackToLibrary(Book book)
    {
        if (!IsBookAvailable(book))
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
        if (IsBookAvailable(book))
        {
            return false;
        }
        
        return true;
    }
    public string DeleteBook()
    {
        var bookForDelete = FindTheBookFromDatabase();

        if (bookForDelete == null)
        {
            return "Book not found.";
        }

        _bookRepository.Delete(bookForDelete.Id);
        _bookRepository.Save();
        return $"Book '{bookForDelete.Title}' by {bookForDelete.Author} has been removed.";
    }
    
    public string BookFinder()
    {
        Book foundBook = FindTheBookFromDatabase();

        if (foundBook == null)
        {
            Console.WriteLine("Book not found in the database.");
            return "Book not found";
        }
        
        return $"Book found: {foundBook.Title} by {foundBook.Author}";
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
            RetrieveUserInput(searchOption ? "Please enter the book title:" : "Please enter the author:");
        
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
            var bookToFind = FindTheBookFromDatabase();

            if (bookToFind == null)
            {
                Console.WriteLine("This book does not exist");
                return;
            }
            
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
                {
                    bookToFind.Title = RetrieveTitle();
                    break;
                }
                case 2:
                {
                    bookToFind.Author = RetrieveAuthor();
                    break;
                }
                case 3:
                {
                    bookToFind.Category = RetrieveCategory();
                    break;
                }
            }
            
            _bookRepository.Update(bookToFind);
            _bookRepository.Save();
            Console.WriteLine("Book updated successfully!");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
    
    public void Borrow()
    {
        Console.WriteLine("What book you want to borrow?");
        
        var book = FindTheBookFromDatabase();

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
    
    
    public string RetrieveTitle()
    {
        var title = RetrieveUserInput("Please enter the name of the book you would like to add");
    
        if (!ConfirmUserInput("This answer is correct? yes/no"))
        {
            CorrectAuthorOrTitleName(title, "title");
        }
        
        return title;
        
    }
    
    public string RetrieveAuthor()
    {
        var author = RetrieveUserInput("Please enter the name of the author you would like to add");

        if (!ConfirmUserInput("This answer is correct? yes/no"))
        {
            CorrectAuthorOrTitleName(author, "author");
        }
        
        return author;
        
    }

    private string CorrectAuthorOrTitleName(string user, string input)
    {
        user = RetrieveUserInput($"Please write correct {input}");

        var check = ConfirmUserInput(user);

        while (!check)
        {
            user = RetrieveUserInput($"Please write correct {input}!");
                    
            if (ConfirmUserInput("This answer is correct? yes/no"))
            {
                break;
            }
        }
        
        return user;
    }
    
    /*private string GetUserInput(string user)
{
    string userInput;
    do
    {
        Console.WriteLine(user);
        userInput = Console.ReadLine();
    } while (string.IsNullOrEmpty(userInput));

    return userInput;
}*/
    
    /*private bool ConfirmUserInput(string user)
    {
        string userInput;
        do
        {
            Console.WriteLine(user);
            userInput = Console.ReadLine().ToLower();
        } while (userInput != "yes" && userInput != "no");

        return userInput == "yes";
    }*/

    public string RetrieveCategory()
    {
        var category = RetrieveUserInputForInt("Please enter the number of the category of this book. We have five category" 
                                          + "\n1. Fantasy"
                                          + "\n2. Sci-Fi"
                                          + "\n3. Thriller/Crime"
                                          + "\n4. Romance"
                                          + "\n5. Young Adult", 1, 5);


        if (!ConfirmUserInput("This answer is correct? yes/no"))
        {
            CorrectCategory(category);
        }
        
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

    private string CorrectCategory(string category)
    {
        category = RetrieveUserInputForInt("Please write correct category", 1, 5);

        if (!ConfirmUserInput("This answer is correct? yes/no"))
        {
            while (true)
            {
                category = RetrieveUserInputForInt("Please write correct category!", 1, 5);
                    
                if (ConfirmUserInput("This answer is correct? yes/no"))
                {
                    break;
                }
            }
        } 
        
        return category;
    }

    public long RetrieveISBN()
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
    
    private string RetrieveUserInput(string user)
    {
        string userInput;
        do
        {
            Console.WriteLine(user);
            userInput = Console.ReadLine();
        } while (string.IsNullOrEmpty(userInput));

        return userInput;
    }
    
    private string RetrieveUserInputWithConfirmation(string user)
    {
        string userInput;
        do
        {
            Console.WriteLine(user);
            userInput = Console.ReadLine().Trim();
        } while (string.IsNullOrEmpty(userInput) || !ConfirmUserInput($"Is '{userInput}' correct? (yes/no)"));

        return userInput;
    }
    
    private string RetrieveUserInputForInt(string prompt, int min, int max)
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