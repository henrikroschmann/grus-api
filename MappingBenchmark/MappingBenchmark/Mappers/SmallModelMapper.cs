using MappingBenchmark.Dto;
using MappingBenchmark.Models;
using Riok.Mapperly.Abstractions;

namespace MappingBenchmark.Mappers;

public static class SmallModelMapper
{
    public static SmallModelDTO MapToDTO(SmallModel model)
    {
        return new SmallModelDTO
        {
            Id = model.Id,
            Name = model.Name
        };
    }
}

[Mapper]
public partial class SmallModelMapperly
{
    public partial SmallModelDTO MapToDTO(SmallModel model);
}