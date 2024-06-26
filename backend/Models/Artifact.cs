namespace art_gallery.Models
{
    public class Artifact
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }  
        public required string Artist { get; set; }
        public required string Description { get; set; }
    }
}
