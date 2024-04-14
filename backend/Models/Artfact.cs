namespace art_gallery.Models
{
    public class Artfact
    {
        public required int Id { get; set; }
        public required int Year { get; set; }
        public required string Image { get; set; }
        public required string Description { get; set; }
    }
}
