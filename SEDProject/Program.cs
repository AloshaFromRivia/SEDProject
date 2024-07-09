using Microsoft.EntityFrameworkCore;
using SEDProject.Models.Database;
using SEDProject.Models.Entities;
using SEDProject.Models.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer());

//DI
builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IRepository<Department>, DepartmentRepository>();
builder.Services.AddTransient<IRepository<Participant>, ParticipantRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// обработка ошибок HTTP
app.UseStatusCodePages(async statusCodeContext =>
{
    var response = statusCodeContext.HttpContext.Response;
    var path = statusCodeContext.HttpContext.Request.Path;

    response.ContentType = "text/plain; charset=UTF-8";
    if (response.StatusCode == 403)
    {
        await response.WriteAsync($"Path: {path}. Access Denied ");
    }
    else if (response.StatusCode == 404)
    {
        await response.WriteAsync($"Resource {path} Not Found");
    }
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.Run();