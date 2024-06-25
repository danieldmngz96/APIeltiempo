namespace APIElTiempo.Models
{
    public class ArticleImage
    {
        public int ImageId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string ImagePath { get; set; }

    }
}
