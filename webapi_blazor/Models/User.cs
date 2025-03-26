//code first
using System.ComponentModel.DataAnnotations;

public class User{
    [Key]
    public int Id  { get; set; }
    public string Name { get; set; }
    public int Age  { get; set; }
    public string Email { get; set; }
    public string Additional {get;set;}
}