namespace art_gallery.Models
{
    public class ArtStyle
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Image { get; set; }  
        public required string Creator { get; set; }
    }
}
