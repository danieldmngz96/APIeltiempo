using APIeltiempo.Models;
using APIElTiempo.Models;
using APIElTiempo.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    public string _connectionString = "Data Source=172.16.91.126;Initial Catalog=Parqueoo;User ID=usrParqueoo;Password=Us3rP4rqu300*;MultipleActiveResultSets=True;TrustServerCertificate=True;";
    private readonly IArticleService _articleService;


    public ArticlesController(IArticleService articleService)

    {
        _articleService = articleService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ArticleDto>>> GetArticles()
    {
        var articles = await _articleService.GetAllArticlesAsync();
        return Ok(articles);
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

    [HttpPost("api/articles")]
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

    [HttpPost("{id}/upload")]
    public async Task<IActionResult> UploadFile(int id, [FromForm] FileUploadDto fileDto)
    {
        if (fileDto == null || fileDto.File == null || fileDto.File.Length == 0)
        {
            return BadRequest("No se cargó ningún archivo o el archivo está vacío.");
        }

        try
        {
            var imageUrl = await SaveFileAndGetUrl(fileDto.File); // Llamada corregida al método SaveFileAndGetUrl
            var article = await _articleService.GetArticleByIdAsync(id);

            if (article == null)
            {
                return NotFound();
            }

            // Actualizar el campo de la URL o referencia del archivo multimedia en el artículo
            article.ImageUrl = imageUrl; // Suponiendo que ImageUrl es el campo en ArticleDto para la imagen

            await _articleService.UpdateArticleAsync(id, article);

            return Ok(new { imageUrl });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }


    // Método para guardar el archivo y obtener la URL
    private async Task<string> SaveFileAndGetUrl(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            throw new ArgumentException("El archivo proporcionado es nulo o está vacío.");
        }

        // Aquí implementarías la lógica real para guardar el archivo y obtener su URL o referencia
        // Por ejemplo, guardar en un servicio de almacenamiento en la nube y retornar la URL de acceso
        // En esta implementación ficticia, simplemente devuelvo una URL de ejemplo
        return await Task.FromResult("https://example.com/image.jpg");
    }

}

