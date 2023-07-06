using BE_CRMColegio.Controllers;
using BE_CRMColegio.Repository;
using BE_CRMColegio.Repository.Curso;
using BE_CRMColegio.Repository.Docente;
using BE_CRMColegio.Repository.Estudiante;
using BE_CRMColegio.Repository.Horario;
using BE_CRMColegio.Repository.Mensaje;
using BE_CRMColegio.Repository.Mensajes;
using BE_CRMColegio.Repository.SalonClases;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Cors
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
    builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

//Add Context BD
var MySqlConfiguration = new MySQLConfiguration(builder.Configuration.GetConnectionString("conexion"));
builder.Services.AddSingleton(MySqlConfiguration);

//Configuracion de swagger 
builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});


//Add Services
builder.Services.AddScoped<IPadreRepository,PadreRepository>();
builder.Services.AddScoped<IEstudianteRepository,EstudianteRepository>();
builder.Services.AddScoped<IDocenteRepository,DocenteRepository>();
builder.Services.AddScoped<ISalonClasesRepository,SalonClasesRepository>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<IHorarioRepository, HorarioRepository>();
builder.Services.AddScoped<IMensajeRepository, MensajeRepository>();

var app = builder.Build();


    

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors("CorsPolicy");

app.MapControllers();

app.Run();
