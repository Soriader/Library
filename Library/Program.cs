// See https://aka.ms/new-console-template for more information

using Library;
using Library.Models;

Console.WriteLine("Welcome to the Library!");

var bookService = new BookService();
var findTheBook = new FindTheBook();
var bookRepository = new BookRepository();

findTheBook.BookFinder();

/*var book = new Book(bookService.GetTitle(), bookService.GetAuthor(), bookService.GetCategory());
book.IsAvailable = true;
bookRepository.Insert(book);
bookRepository.Save();*/