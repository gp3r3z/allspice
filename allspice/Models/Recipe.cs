
namespace allspice.Models;
public class Recipe
{
    public int Id { get; set; }

    public string Title { get; set; }
    [Required]
    public string Instructions { get; set; }
    public string Img { get; set; } = "https://images.unsplash.com/photo-1575485670541-824ff288aaf8?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1074&q=80";
    public string Category { get; set; }
    public string CreatorId { get; set; }
    public Account Creator { get; set; }
}


