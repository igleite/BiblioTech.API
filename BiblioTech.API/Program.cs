using BiblioTech.API.Configuration;
using BiblioTech.API.Persistence;
using BiblioTech.API.Validations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssemblyContaining<BookValidator>();
builder.Services.AddFluentValidationAutoValidation();

builder.Services.AddControllers();

builder.Services.AddDbContext<BiblioTechDbContext>(options =>
    options.UseMySql("server=localhost;port=3307;database=meubanco;uid=root;password=alunos", ServerVersion.AutoDetect("server=localhost;port=3307;database=meubanco;uid=root;password=alunos")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Total",
        policyBuilder =>
            policyBuilder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors("Total");

app.UseAuthorization();

app.MapControllers();

app.Run();
