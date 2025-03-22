using Library.Models;

namespace Library;

public class AddNewBook
{
    private readonly BookRepository _bookRepository;
    public AddNewBook()
    {
        _bookRepository = new BookRepository();
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
                                          + "\n5. Young Adult");

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
            category = GetUserInputForInt("Please write correct category");

            if (ConfirmUserInput("This answer is correct? yes/no"))
            {
                return category;
            }
            else
            {
                while (true)
                {
                    category = GetUserInputForInt("Please write correct author!");
                    
                    if (ConfirmUserInput("This answer is correct? yes/no"))
                    {
                        break;
                    }
                }
                return category;
            }
        }
    }

    public int GetISBN()
    {
        int code = CorrectISBN("Please enter the ISBN of the book you would like to add" +
                    "\nThis code need 13 numbers");

        return code;
    }


    private int CorrectISBN(string ISBN)
    {
        return 1;
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
    
    private string GetUserInputForInt(string prompt)
    {
        string userInput;
        int number;
        bool isValid;

        do
        {
            Console.WriteLine(prompt); 
            userInput = Console.ReadLine();

            bool isNumber = int.TryParse(userInput, out number);

            isValid = isNumber && number >= 1 && number <= 5;

            if (!isValid)
            {
                Console.WriteLine("Error: Enter a number from 1 to 5.");
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