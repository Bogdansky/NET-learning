namespace Infrastructure.Models
{
    public class FileCreatedMessage
    {
        public string Name { get; }
        public long ChunksNumber { get; }

        public FileCreatedMessage(string name, long chunksNumber)
        {
            Name = name;
            ChunksNumber = chunksNumber;
        }
    }
}
