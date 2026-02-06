using VehicleFinder.Extensions;
using Vehicles.Infrastructure.EF;
using Vehicles.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

// Protección de variable de entorno para pruebas
var integrationTestEnv = Environment.GetEnvironmentVariable("INTEGRATIONTEST");
var isIntegrationTest = integrationTestEnv != null && integrationTestEnv.Equals("isTest");

// Controllers y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar MediatR solo **después de los repositorios**
// Aquí lo dejamos pendiente porque en IntegrationTest se registrará en la fábrica

// Registrar módulos solo si **no estamos en pruebas**
if (!isIntegrationTest)
{
    builder.Services.AddVehiclesModule(builder.Configuration);
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
