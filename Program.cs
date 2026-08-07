using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//configuracao ef core - banco de dados
builder.Services.AddDbContext<EventContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//injecao de dependencia
//addScopped significa que uma instancia(os metodos la de cadastrar, listar) nova é criada por requisicao http
//isso garante que cada requisicao tenha seu proprio 
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
