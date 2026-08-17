using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

//configuracao ef core - banco de dados
builder.Services.AddDbContext<EventContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//add aki
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        //corta o loop 
        //coloca null onde a referencia se repete
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

//injecao de dependencia
//addScopped significa que uma instancia(os metodos la de cadastrar, listar) nova é criada por requisicao http
//isso garante que cada requisicao tenha seu proprio 
builder.Services.AddScoped<ITipoUsuario, TipoUsuarioRepository>();
builder.Services.AddScoped<ITipoEvento, TipoEventoRepository>();
builder.Services.AddScoped<IUsuario, UsuarioRepository> ();

//Registra o serviço de controllers(mapeia automaticamente os controllers da pasta /Controllers)
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

//Mapeia as rotas definidas nos Controllers com os atributos [Route]: api/[controller]
app.MapControllers();

app.Run();
