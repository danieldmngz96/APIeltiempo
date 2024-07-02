using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using APIElTiempo.Context;
using APIElTiempo.Models;
using APIElTiempo.Services;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add DbContext configuration
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Add services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IArticleService, ArticleService>();

// Configurar servicios CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", builder =>
    {
        builder.WithOrigins("https://localhost:7296") // Reemplaza con tu origen específico permitido
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// Add controllers
builder.Services.AddControllers();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Content Management API", Version = "v1" });
});

// Add Endpoints API Explorer
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Content Management API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();
// Use the CORS policy
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
