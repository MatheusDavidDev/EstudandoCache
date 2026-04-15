using CacheApi.Repositories;
using CacheApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region [DI]   
builder.Services.AddMemoryCache();
builder.Services.AddTransient<IProdutoService, ProdutoService>();
builder.Services.AddTransient<ICacheService, CacheService>();
#endregion

builder.Services.Configure<CsvConfig>(
    builder.Configuration.GetSection("Csv"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
