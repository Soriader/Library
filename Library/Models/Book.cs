namespace Library.Models;

public class Book
{

    public Book(string title, string author, string category, long ISBN)
    {
        this.Title = title;
        this.Author = author;
        this.Category = category;
        this.ISBN = ISBN;
    }
    
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public long ISBN { get; set; }
    public string Category { get; set; }
    public bool IsAvailable { get; set; }
}