using APIElTiempo.Context;
using APIElTiempo.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace APIElTiempo.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArticleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ArticleDto>> GetAllArticlesAsync()
        {
            var articles = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.ArticleImages)
                .ToListAsync();
            return _mapper.Map<IEnumerable<ArticleDto>>(articles);
        }

        public async Task<ArticleDto> GetArticleByIdAsync(int articleId)
        {
            var article = await _context.Articles
                .Include(a => a.Author)
                .Include(a => a.ArticleImages)
                .FirstOrDefaultAsync(a => a.ArticleId == articleId);
            if (article == null)
            {
                throw new KeyNotFoundException("Article not found");
            }
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task<ArticleDto> CreateArticleAsync(ArticleDto articleDto)
        {
            var article = _mapper.Map<Article>(articleDto);
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();
            return _mapper.Map<ArticleDto>(article);
        }

        public async Task UpdateArticleAsync(int articleId, ArticleDto articleDto)
        {
            var article = await _context.Articles.FindAsync(articleId);
            if (article == null)
            {
                throw new KeyNotFoundException("Article not found");
            }
            _mapper.Map(articleDto, article);
            _context.Articles.Update(article);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteArticleAsync(int articleId)
        {
            var article = await _context.Articles.FindAsync(articleId);
            if (article == null)
            {
                throw new KeyNotFoundException("Article not found");
            }
            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();
        }



    }
}
