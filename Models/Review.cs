namespace EcommerceEjemploApi.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateTime { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
