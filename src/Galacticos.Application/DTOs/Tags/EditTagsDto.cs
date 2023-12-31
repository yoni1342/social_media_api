namespace Galacticos.Application.DTOs.Tags
{
    public class EditTagsDto
    {
        public Guid PostId { get; set; }
        public Guid TagId { get; set; }
        public string Name { get; set; } = null!;
    }
}