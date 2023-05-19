namespace RestArchitecture.Models
{
    public class CategoryDto
    {
        public string Name { get; }
        public CategoryDto(string name)
        {
            Name = name;
        }
    }
}
