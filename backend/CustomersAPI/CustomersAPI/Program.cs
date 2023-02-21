using CustomersAPI.Implementaciones;
using CustomersAPI.Repositorios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Permite configurar el URL para pasar de api/Clientes a api/clientes
builder.Services.AddRouting(routing => routing.LowercaseUrls = true);

builder.Services.AddDbContext<ClienteBasedatosContenido>(mysqlBuilder =>
{
    // conexion a la base de datos con buenas practicas
    mysqlBuilder.UseMySQL(builder.Configuration.GetConnectionString("ConnectionSQL"));
});

// implementamos la actualizacion y la interfaz
builder.Services.AddScoped<InterfazActualizarImple, ActualizarImplementacion>();

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
