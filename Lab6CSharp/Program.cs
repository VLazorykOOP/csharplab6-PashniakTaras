using System;
using System.Collections;

// Завдання 1: Побудова ієрархії класів
// Базовий клас
public class Item
{
    public string Name;
    public double Price;

    public Item(string name, double price)
    {
        Name = name;
        Price = price;
    }

    public virtual void Show()
    {
        Console.WriteLine($"Item: {Name}, Price: {Price}");
    }
}

// Похідні класи
public class Toy : Item
{
    protected string Manufacturer;

    public Toy(string name, double price, string manufacturer) : base(name, price)
    {
        Manufacturer = manufacturer;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Manufacturer: {Manufacturer}");
    }
}

public class Product : Item
{
    protected string Category;

    public Product(string name, double price, string category) : base(name, price)
    {
        Category = category;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Category: {Category}");
    }
}

public class GroceryProduct : Product
{
    protected string ExpiryDate;

    public GroceryProduct(string name, double price, string category, string expiryDate) : base(name, price, category)
    {
        ExpiryDate = expiryDate;
    }

    public override void Show()
    {
        base.Show();
        Console.WriteLine($"Expiry Date: {ExpiryDate}");
    }
}

// Завдання 2: Побудова ієрархії з інтерфейсом
// Абстрактний клас Клієнт
public abstract class Client : IComparable
{
    public string LastName;
    protected DateTime OpeningDate;

    public Client(string lastName, DateTime openingDate)
    {
        LastName = lastName;
        OpeningDate = openingDate;
    }

    public abstract void DisplayInfo();
    public abstract bool MatchesCriteria(DateTime date);
    public abstract int CompareTo(object obj);
}

// Похідні класи
public class Depositor : Client
{
    protected double DepositAmount;
    protected double DepositInterest;

    public Depositor(string lastName, DateTime openingDate, double depositAmount, double depositInterest) : base(lastName, openingDate)
    {
        DepositAmount = depositAmount;
        DepositInterest = depositInterest;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Depositor: {LastName}, Opening Date: {OpeningDate}, Deposit Amount: {DepositAmount}, Deposit Interest: {DepositInterest}");
    }

    public override bool MatchesCriteria(DateTime date)
    {
        return OpeningDate.Date == date.Date;
    }

    public override int CompareTo(object obj)
    {
        if (obj == null) return 1;

        Client otherClient = obj as Client;
        if (otherClient != null)
            return this.LastName.CompareTo(otherClient.LastName);
        else
            throw new ArgumentException("Object is not a Client");
    }
}

public class Creditor : Client
{
    protected double LoanAmount;
    protected double LoanInterest;
    protected double RemainingBalance;

    public Creditor(string lastName, DateTime openingDate, double loanAmount, double loanInterest, double remainingBalance) : base(lastName, openingDate)
    {
        LoanAmount = loanAmount;
        LoanInterest = loanInterest;
        RemainingBalance = remainingBalance;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Creditor: {LastName}, Opening Date: {OpeningDate}, Loan Amount: {LoanAmount}, Loan Interest: {LoanInterest}, Remaining Balance: {RemainingBalance}");
    }

    public override bool MatchesCriteria(DateTime date)
    {
        return OpeningDate.Date == date.Date;
    }

    public override int CompareTo(object obj)
    {
        if (obj == null) return 1;

        Client otherClient = obj as Client;
        if (otherClient != null)
            return this.LastName.CompareTo(otherClient.LastName);
        else
            throw new ArgumentException("Object is not a Client");
    }
}

public class Organization : Client
{
    protected string Name;
    protected string AccountNumber;
    protected double AccountBalance;

    public Organization(string name, DateTime openingDate, string accountNumber, double accountBalance) : base(name, openingDate) // Empty string for LastName as it's not applicable for organizations
    {
        Name = name;
        AccountNumber = accountNumber;
        AccountBalance = accountBalance;
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"Organization: {Name}, Opening Date: {OpeningDate}, Account Number: {AccountNumber}, Account Balance: {AccountBalance}");
    }

    public override bool MatchesCriteria(DateTime date)
    {
        return OpeningDate.Date == date.Date;
    }

    public override int CompareTo(object obj)
    {
        if (obj == null) return 1;

        Client otherClient = obj as Client;
        if (otherClient != null)
            return this.Name.CompareTo(otherClient.LastName);
        else
            throw new ArgumentException("Object is not a Client");
    }
}

public class Bank
{
    private Client[] clients;

    public Bank(int size)
    {
        clients = new Client[size];
    }

    public void AddClient(Client client, int index)
    {
        clients[index] = client;
    }

    public void DisplayAllClientsInfo()
    {
        foreach (Client client in clients)
        {
            client.DisplayInfo();
        }
    }

    public void SearchClients(DateTime date)
    {
        Console.WriteLine($"Clients who started collaborating with the bank on {date}:");
        foreach (Client client in clients)
        {
            if (client.MatchesCriteria(date))
                client.DisplayInfo();
        }
    }
}

// Завдання 3: Додавання стандартних інтерфейсів .NET для перерахування
public enum ProductType { Toy, Grocery, Other }

public class StoreItem : IEnumerable
{
    private List<Item> items;

    public StoreItem()
    {
        items = new List<Item>();
    }

    public void AddItem(Item item)
    {
        items.Add(item);
    }

    public IEnumerator GetEnumerator()
    {
        return items.GetEnumerator();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("What task do you want?");
        Console.WriteLine("1. Task 1");
        Console.WriteLine("2. Task 2");
        Console.WriteLine("3. Task 3");
        Console.WriteLine("4. Exit");

        int choice;
        bool isValidChoice = false;

        do
        {
            Console.Write("Enter number of task: ");
            isValidChoice = int.TryParse(Console.ReadLine(), out choice);

            if (!isValidChoice || choice < 1 || choice > 4)
            {
                Console.WriteLine("This task does not exist");
                isValidChoice = false;
            }
        } while (!isValidChoice);

        switch (choice)
        {
            case 1:
                task1();
                break;
            case 2:
                task2();
                break;
            case 3:
                task3();
                break;
            case 4:
                break;
        }
    }

    static void task1()
    {
        // Тестування завдання 1
        Toy toy = new Toy("Action Figure", 15.99, "Hasbro");
        toy.Show();

        Product product = new Product("Book", 9.99, "Literature");
        product.Show();

        GroceryProduct groceryProduct = new GroceryProduct("Milk", 2.49, "Dairy", "2024-04-30");
        groceryProduct.Show();
    }

    static void task2()
    {
        // Тестування завдання 2
        Bank bank = new Bank(3);

        bank.AddClient(new Depositor("Smith", new DateTime(2020, 1, 1), 10000, 5), 0);
        bank.AddClient(new Creditor("Johnson", new DateTime(2021, 3, 15), 20000, 7, 15000), 1);
        bank.AddClient(new Organization("ABC Inc.", new DateTime(2019, 5, 20), "123456", 50000), 2);

        bank.DisplayAllClientsInfo();

        Console.WriteLine();

        bank.SearchClients(new DateTime(2020, 1, 1));
    }

    static void task3()
    {
        // Тестування завдання 3
        StoreItem store = new StoreItem();

        store.AddItem(new Item("Toy Car", 5.99));
        store.AddItem(new Item("Doll", 8.99));
        store.AddItem(new Item("Candy", 2.49));

        foreach (Item item in store)
        {
            Console.WriteLine($"Item: {item.Name}, Price: {item.Price}");
        }
    }
}
