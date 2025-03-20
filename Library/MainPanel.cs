﻿using Library.Models;

namespace Library;

public class MainPanel
{
    private readonly AddNewBook _addNewBook;
    private readonly FindTheBook _findTheBook;
    private readonly EditTheExistBooks _editTheExistBooks;
    private readonly BookRepository _bookRepository;
    
    public MainPanel()
    {
        _addNewBook = new AddNewBook();
        _findTheBook = new FindTheBook();
        _editTheExistBooks = new EditTheExistBooks();
        _bookRepository = new BookRepository();
    }
    
    public void Menu()
    {
        Console.WriteLine("Please enter the name of the category you would like to run:"
                          + "\n1.Add new book"
                          + "\n2.Find the book"
                          + "\n3.Change the properties of the selected book");

        var answer = Console.ReadLine();

        if (int.TryParse(answer, out int option) && (option == 1 || option == 2 || option == 3))
        {
            switch (option)
            {
                case 1:
                {
                    var book = new Book(_addNewBook.GetTitle(), _addNewBook.GetAuthor(), _addNewBook.GetCategory());
                    book.IsAvailable = true;
                    _bookRepository.Insert(book);
                    _bookRepository.Save();
                    Continue();
                    break;
                }

                case 2:
                {
                    _findTheBook.BookFinder();
                    Continue();
                    break;
                }

                case 3:
                {
                    _editTheExistBooks.BookEditor();
                    Continue();
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Invalid option selected.");
            Menu();
        }
    }

    private void Continue()
    {
        Console.WriteLine("You want to do something else or exit?"
                          + "\n1.Back to Main Menu"
                          + "\n2.Exit");

        var answer = Console.ReadLine();

        if (!int.TryParse(answer, out int option) || (option != 1 && option != 2))
        {
            while (true)
            {
                Console.WriteLine("Please write correct number!");
                answer = Console.ReadLine();

                if (int.TryParse(answer, out option) && (option == 1 || option == 2))
                {
                    break;
                }
            }
        }

        switch (option)
        {
            case 1:
            {
                Menu();
                break;
            }
            case 2:
            {
                Console.WriteLine("Thanks for visiting!");
                break;
            }
        }
    }
}