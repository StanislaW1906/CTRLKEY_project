namespace CTRLKEY_project.Models.Users;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    
    public string Role { get; set; }
    
    public string FullName { get; set; }
    public string City { get; set; }
    public string NovaPoshtaAddress { get; set; }
    public string PhoneNumber { get; set; }
    
    
}