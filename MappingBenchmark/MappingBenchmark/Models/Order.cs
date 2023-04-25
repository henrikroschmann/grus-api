namespace MappingBenchmark.Models;

public class Order
{
    public int Id { get; set; }
    public Customer Customer { get; set; }
    public List<OrderItem> Items { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
}

public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Address Address { get; set; }
    public List<Order> Orders { get; set; }
}

public class OrderItem
{
    public int Id { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public List<OrderItem> Orders { get; set; }
}
