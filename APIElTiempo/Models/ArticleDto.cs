namespace APIElTiempo.Models
{
    public class ArticleDto
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public ICollection<ArticleImageDto> ArticleImages { get; set; } // Lista de imágenes asociadas al artículo
        public string ImageUrl { get; set; } // URL de una imagen asociada al artículo

        public IFormFile File { get; set; } // Propiedad para el archivo multimedia
    }
}
