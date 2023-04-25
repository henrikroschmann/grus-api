using MappingBenchmark.Dto;
using MappingBenchmark.Models;
using Riok.Mapperly.Abstractions;

namespace MappingBenchmark.Mappers;

public static class LargeModelMapper
{
    public static LargeModelDTO MapToDTO(LargeModel model)
    {
        return new LargeModelDTO
        {
            Id = model.Id,
            Name = model.Name,
            Age = model.Age,
            Email = model.Email,
            Address = model.Address
        };
    }

}

[Mapper]
public partial class LargeModelMapperly
{
    public partial LargeModelDTO MapToDTO(LargeModel model);
}