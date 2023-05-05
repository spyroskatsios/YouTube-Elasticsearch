using Microsoft.EntityFrameworkCore;
using Songs.Api;
using Songs.Api.Elastic;
using Songs.Api.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});
builder.Services.AddScoped<IAppDbContext, AppDbContext>();

builder.Services.AddElasticSearch(builder.Configuration);
builder.Services.AddScoped<IInitializeIndexService, InitializeIndexService>();
builder.Services.AddScoped<ISearchService, SearchService>();

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

using var scope = app.Services.CreateScope();
var initializeIndexService = scope.ServiceProvider.GetRequiredService<IInitializeIndexService>();
await initializeIndexService.Run();

app.Run();