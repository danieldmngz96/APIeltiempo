using APIElTiempo.Models;

namespace APIElTiempo.Services
{
    public interface IArticleService
    {
        Task<IEnumerable<ArticleDto>> GetAllArticlesAsync();
        Task<ArticleDto> GetArticleByIdAsync(int articleId);
        Task<ArticleDto> CreateArticleAsync(ArticleDto articleDto);
        Task UpdateArticleAsync(int articleId, ArticleDto articleDto);
        Task DeleteArticleAsync(int articleId);

    }
}
