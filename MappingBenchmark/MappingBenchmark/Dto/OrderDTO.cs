namespace MappingBenchmark.Dto;

public class OrderDTO
{
    public int Id { get; set; }
    public CustomerDTO Customer { get; set; }
    public List<OrderItemDTO> Items { get; set; }
    public string OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
}

public class CustomerDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public AddressDTO Address { get; set; }
}

public class OrderItemDTO
{
    public int Id { get; set; }
    public ProductDTO Product { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}

public class ProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
}

public static class OrderMapper
{
    public static OrderDTO MapToDTO(Order order)
    {
        return new OrderDTO
        {
            Id = order.Id,
            Customer = MapToDTO(order.Customer),
            Items = order.Items.Select(item => MapToDTO(item)).ToList(),
            OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
            TotalPrice = order.TotalPrice
        };
    }

    public static CustomerDTO MapToDTO(Customer customer)
    {
        return new CustomerDTO
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Address = MapToDTO(customer.Address)
        };
    }

    public static OrderItemDTO MapToDTO(OrderItem item)
    {
        return new OrderItemDTO
        {
            Id = item.Id,
            Product = MapToDTO(item.Product),
            Quantity = item.Quantity,
            Price = item.Price
        };
    }

    public static ProductDTO MapToDTO(Product product)
    {
        return new ProductDTO
        {
            Id = product.Id,
            Name = product.Name,
            Price = product.Price
        };
    }

    public static AddressDTO MapToDTO(Address address)
    {
        return new AddressDTO
        {
            Street = address.Street,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode
        };
    }
}
