using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Repositories;
using StudentsManager.Data.Data;
using StudentsManager.Data.Repositories;

const string connectionStringName = "StudentsManagement";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly(), typeof(Student).Assembly);

string? connectionString = builder.Configuration.GetConnectionString(connectionStringName);
builder.Services.AddDbContext<StudentsManagementContext>(optionsBuilder 
    => optionsBuilder.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepository<Student>, Repository<Student>>();
builder.Services.AddScoped<IRepository<Course>, Repository<Course>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Students}/{action=Index}/{id?}");

app.Run();