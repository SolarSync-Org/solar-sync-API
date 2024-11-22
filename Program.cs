using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SolarSync_API.Services;
using SolarSync_API.Data;
using SolarSync_API.Interface;

var builder = WebApplication.CreateBuilder(args);

// Adiciona controllers
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SolarSync_API", Version = "v1" });

    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

// Configuração de MongoDB
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var connectionString = builder.Configuration.GetConnectionString("DbConnection") 
                           ?? throw new InvalidOperationException("A string de conexão com o MongoDB está ausente.");
    return new MongoClient(connectionString);
});

// Registro do IMongoDatabase usando o nome do banco de dados incluído na string de conexão
builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("SolarSync"); // Nome do banco de dados diretamente
});

// Registro do MongoDbService como Singleton, se necessário
builder.Services.AddSingleton<MongoDbService>();

// Registro de serviços de negócio
builder.Services.AddScoped<ICompanyReportService, CompanyReportService>();
builder.Services.AddScoped<IClientReportService, ClientReportService>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<CompanyService>();  
builder.Services.AddScoped<ClientService>();  
builder.Services.AddHttpClient<ValidationService>();
builder.Services.AddScoped<ValidationService>();

var app = builder.Build();

// Configuração do pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.MapControllers();
app.Run();
