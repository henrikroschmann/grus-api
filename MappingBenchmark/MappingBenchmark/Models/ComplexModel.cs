namespace MappingBenchmark.Models;

public class ComplexModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<SmallModel> SmallModels { get; set; }
    public LargeModel LargeModel { get; set; }
}