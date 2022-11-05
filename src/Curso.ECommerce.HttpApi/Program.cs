using System.Diagnostics;
using Curso.ECommerce.Application.Service;
using Curso.ECommerce.Domain.Repository;
using Curso.ECommerce.Infraestructure;
using Curso.ECommerce.Infraestructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ECommerceDbContext>(options =>
{
    var folder = Environment.SpecialFolder.LocalApplicationData;
    var path = Environment.GetFolderPath(folder);
    var dbPath = Path.Join(path, builder.Configuration.GetConnectionString("ECommerce"));
    Debug.WriteLine($"dbPath: {dbPath}");

    options.UseSqlite($"Data Source={dbPath}");
});
builder.Services.AddTransient<IBrandRepository, BrandRepository>();
builder.Services.AddTransient<IProductTypeRepository, ProductTypeRepository>();

builder.Services.AddTransient<IBrandAppService, BrandAppService>();
builder.Services.AddTransient<IProductTypeAppService, ProductTypeAppService>();

builder.Services.AddScoped<IUnitOfWork>(provider => 
{
    var instance = provider.GetService<ECommerceDbContext>();
    return instance;
});

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
