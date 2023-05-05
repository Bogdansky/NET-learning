namespace Infrastructure.Models
{
    public class FileCreatedMessage
    {
        public string Name { get; }
        public int ChunksNumber { get; }

        public FileCreatedMessage(string name, int chunksNumber)
        {
            Name = name;
            ChunksNumber = chunksNumber;
        }
    }
}
