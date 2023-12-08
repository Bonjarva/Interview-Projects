namespace ContactListApp.Models;

public class Contact
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public required string PhoneNumber { get; set; }
    public DateTime DOB { get; set; }
}