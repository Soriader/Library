namespace Library.Models;

public class Book
{

    public Book(string title, string author)
    {
        this.Title = title;
        this.Author = author;
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
}