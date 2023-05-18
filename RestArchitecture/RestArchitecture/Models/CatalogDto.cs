namespace RestArchitecture.Models
{
    public class CatalogDto
    {
        public string Name { get; }
        public CatalogDto(string name)
        {
            Name = name;
        }
    }
}
