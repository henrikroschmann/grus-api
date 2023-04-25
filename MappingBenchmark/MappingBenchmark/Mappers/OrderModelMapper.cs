using MappingBenchmark.Dto;
using MappingBenchmark.Models;

namespace MappingBenchmark.Mappers;

public static class OrderMapper
{
    public static OrderDTO MapToDTO(Order order)
    {
        var customerDTO = new CustomerDTO
        {
            Id = order.Customer.Id,
            FirstName = order.Customer.FirstName,
            LastName = order.Customer.LastName,
            Address = MapAddressToDTO(order.Customer.Address),
        };

        var itemsDTO = order.Items.Select(MapOrderItemToDTO).ToList();

        return new OrderDTO
        {
            Id = order.Id,
            Customer = customerDTO,
            Items = itemsDTO,
            OrderDate = order.OrderDate.ToString("yyyy-MM-dd"),
            TotalPrice = order.TotalPrice
        };
    }

    private static AddressDTO MapAddressToDTO(Address address)
    {
        return new AddressDTO
        {
            Street = address.Street,
            City = address.City,
            State = address.State,
            ZipCode = address.ZipCode
        };
    }

    private static OrderItemDTO MapOrderItemToDTO(OrderItem orderItem)
    {
        var productDTO = new ProductDTO
        {
            Id = orderItem.Product.Id,
            Name = orderItem.Product.Name,
            Price = orderItem.Product.Price
        };

        return new OrderItemDTO
        {
            Id = orderItem.Id,
            Product = productDTO,
            Quantity = orderItem.Quantity,
            Price = orderItem.Price
        };
    }
}
