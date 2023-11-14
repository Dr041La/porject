namespace rusiVSlizards.ViewModels;

public class Account
{
    public int id { get; set; }
    public int userId { get; set; }
    public int balance { get; set; }

    public Account(int id, int userId, int balance)
    {
        this.id = id;
        this.userId = userId;
        this.balance = balance;
    }
}