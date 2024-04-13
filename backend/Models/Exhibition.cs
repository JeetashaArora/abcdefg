namespace art_gallery.Models
{
    public class Exhibition
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required string Place { get; set; }
        public required DateTime Date { get; set; }
    }
}
