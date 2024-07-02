using System.ComponentModel.DataAnnotations;

namespace APIElTiempo.Models
{
    public class ArticleImage
    {
        [Key]
        public int ImageId { get; set; }
        public int ArticleId { get; set; }
        public Article Article { get; set; }
        public string ImagePath { get; set; }

    }
}
