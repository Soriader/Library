// See https://aka.ms/new-console-template for more information

using Library;
using Library.Models;

Console.WriteLine("Welcome to the Library!");

var bookService = new BookService();
var bookRepository = new BookRepository();
var book = new Book(bookService.GetTitle(), bookService.GetAuthor());
book.IsAvailable = true;
bookRepository.Insert(book);
bookRepository.Save();