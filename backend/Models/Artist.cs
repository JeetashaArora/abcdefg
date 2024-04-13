namespace art_gallery.Models
{
    public class Artist
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Image { get; set; }  
        public required string Description { get; set; }
        public required int Artifact_Count { get; set; }
    }
}
