using rusiVSlizards.ViewModels;

namespace rusiVSlizards.Models;

public class Client
{
    public int id;
    public string name { get; set; }
    public string password { get; set; }
    public UserRole userRole { get; set; }
    
    public Client(int id, string name, string password, UserRole userRole)
    {
        this.id = id;
        this.name = name;
        this.password = password;
        this.userRole = userRole;
    }
    
    public Client()
    {}

    public override string ToString()
    {
        return name;
    }
}

public enum UserRole
{
    admin,
    accountant,
    employee,
    client
}