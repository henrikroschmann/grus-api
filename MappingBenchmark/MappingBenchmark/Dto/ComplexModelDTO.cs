namespace MappingBenchmark.Dto;

public class ComplexModelDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SmallModelDTO> SmallModels { get; set; }
    public LargeModelDTO LargeModel { get; set; }
}