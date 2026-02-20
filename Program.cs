using IMS.PRODUCTAPI.Data;
using Microsoft.EntityFrameworkCore;
using IMS.PRODUCTAPI.Repository;
using IMS.PRODUCTAPI.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Using Swagger UI 
// Register repository for Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

//Inject the database.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Enable Swagger UI
app.UseSwagger();
app.UseSwaggerUI();




app.UseHttpsRedirection();

app.MapControllers();

app.Run();