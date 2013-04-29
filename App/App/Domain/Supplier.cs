namespace App.Domain
{
    public class Supplier
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
    }
}