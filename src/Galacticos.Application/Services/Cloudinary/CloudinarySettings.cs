namespace Galacticos.Application.Cloudinary
{
    public class CloudinarySettings
    {
        public const string SectionName = "Cloudinary settings";
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

}