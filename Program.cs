


using Microsoft.EntityFrameworkCore;
using TodoAPI.Data;
using TodoAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ToDoConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));


// Add services to the container.

builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowAngularDevClient", policy =>
	{
		policy.WithOrigins("http://localhost:4200") // Angular dev server
			  .AllowAnyMethod()
			  .AllowAnyHeader();
	});
});


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoService, TodoService>();

var app = builder.Build();

app.UseCors("AllowAngularDevClient");

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
