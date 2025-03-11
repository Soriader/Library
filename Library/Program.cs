// See https://aka.ms/new-console-template for more information

using Library;
using Library.Models;

Console.WriteLine("Hello, World!");

var bookRepository = new BookRepository();
var book = new Book("Ozyrys", "Mróz Remigiusz");
book.IsAvailable = true;
bookRepository.Insert(book);
bookRepository.Save();