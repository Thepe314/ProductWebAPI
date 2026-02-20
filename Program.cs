using IMS.PRODUCTAPI.Data;
using Microsoft.EntityFrameworkCore;
using IMS.PRODUCTAPI.Repository;
using IMS.PRODUCTAPI.Repositories;


var builder = WebApplication.CreateBuilder(args);

// Add services to the DI container
builder.Services.AddControllers();  // Register MVC controllers
builder.Services.AddEndpointsApiExplorer(); // Enable API endpoint discovery for Swagger
builder.Services.AddSwaggerGen();  // Enable Swagger UI for API documentation

// Register repository for Dependency Injection
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();

//Inject the Database context
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//build the application
var app = builder.Build();

// Enable Swagger UI/Configure the middleware
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS

app.MapControllers();  // Map controller routes

//Run application
app.Run();