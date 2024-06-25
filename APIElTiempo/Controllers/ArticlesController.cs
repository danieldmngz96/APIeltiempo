using APIElTiempo.Models;
using APIElTiempo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    public string _connectionString = "Data Source=172.16.91.126;Initial Catalog=Parqueoo;User ID=usrParqueoo;Password=Us3rP4rqu300*;MultipleActiveResultSets=True;TrustServerCertificate=True;"
    private readonly IArticleService _articleService;

    public ArticlesController(IArticleService articleService)
    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<IEnumerable<ArticleDto>> GetAllArticlesAsync()
    {
        var articles = new List<ArticleDto>();

        using (var connection = new SqlConnection(_connectionString))
        {
            var command = new SqlCommand("SELECT Id, Title, Content, PublishedDate FROM Articles", connection);
            connection.Open();
            using (var reader = await command.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    var article = new ArticleDto
                    {
                        Id = reader.GetInt32(0),
                        Title = reader.GetString(1),
                        Content = reader.GetString(2),
                        PublishedDate = reader.GetDateTime(3)
                    };
                    articles.Add(article);
                }
            }
        }

        return articles;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ArticleDto>> GetArticle(int id)
    {
        var article = await _articleService.GetArticleByIdAsync(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article);
    }

    [HttpPost]
    public async Task<ActionResult<ArticleDto>> CreateArticle(ArticleDto articleDto)
    {
        var article = await _articleService.CreateArticleAsync(articleDto);
        return CreatedAtAction(nameof(GetArticle), new { id = article.ArticleId }, article);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateArticle(int id, ArticleDto articleDto)
    {
        if (id != articleDto.ArticleId)
        {
            return BadRequest();
        }
        await _articleService.UpdateArticleAsync(id, articleDto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteArticle(int id)
    {
        await _articleService.DeleteArticleAsync(id);
        return NoContent();
    }
}

