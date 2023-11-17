namespace rusiVSlizards.Models;

public class Account
{
    public int id;
    public Client user;

    public string User
    {
        get
        {
            return user.ToString();
        }
    }
    public decimal balance { get; set; }

    public Account(int id, Client user, decimal balance)
    {
        this.id = id;
        this.user = user;
        this.balance = balance;
    }

    public Account() 
    { }

    public override string ToString()
    {
        return User;
    }
}