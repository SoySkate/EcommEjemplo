﻿namespace EcommerceEjemploApi.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateTime { get; set; }

        //foreign keys:
        public int UserId { get; set; }
        public int ProductId { get; set; }
    }
}
