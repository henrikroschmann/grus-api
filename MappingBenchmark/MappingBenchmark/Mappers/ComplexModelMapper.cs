using MappingBenchmark.Dto;
using MappingBenchmark.Models;
using Riok.Mapperly.Abstractions;

namespace MappingBenchmark.Mappers;

public static class ComplexModelMapper
{
    public static ComplexModelDTO MapToDTO(ComplexModel model)
    {
        var smallModelDTOs = model.SmallModels.Select(SmallModelMapper.MapToDTO).ToList();
        var largeModelDTO = LargeModelMapper.MapToDTO(model.LargeModel);

        return new ComplexModelDTO
        {
            Id = model.Id,
            Name = model.Name,
            SmallModels = smallModelDTOs,
            LargeModel = largeModelDTO
        };
    }
}

[Mapper]
public partial class ComplexModelMapperly
{
    public partial ComplexModelDTO MapToDTO(ComplexModel model);
}