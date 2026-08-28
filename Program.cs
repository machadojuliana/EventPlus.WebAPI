using CloudinaryDotNet;
using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Repositories;
using EventPlus.WebAPI.Services;
using EventPlus.WebAPI.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// adcionando a swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Insira um token válido para ter acesso aos endpoints da API"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement {
        [new OpenApiSecuritySchemeReference ("Bearer, document")] = []
    });
});

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
builder.Services.AddScoped<IEvento, EventoRepository> ();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService >();
builder.Services.AddScoped<IComentario, ComentarioRepository>();

// autenticacao jwt
// configura como a api vai validar  os tokens recebidos nas requisicoes
builder.Services.AddAuthentication(options => {
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // valida quem emitiu o token 
            ValidateIssuer = true,
            ValidIssuer = "EventPlus.WebAPI",

            // valida para quem o token foi emitido
            ValidateAudience = true,
            ValidAudience = "EventPlus.WebAPI",

            // valida se o token ainda esta dentro do prazo de validade
            ValidateLifetime = true,

            // define a tolerancia de clock entre servidores
            ClockSkew = TimeSpan.FromMinutes(5),

            // chave secreta utilizada para validar a assisnatura do token
            IssuerSigningKey = new SymmetricSecurityKey(
                System.Text.Encoding.UTF8.GetBytes("Jwt:Key")
                )
        };
    });

// config cloudinary
builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("Cloudinary"));

// --- Sightengine (plano Free, sem cartão) ---
builder.Services.Configure<SightengineSettings>(builder.Configuration.GetSection("Sightengine"));

builder.Services.AddHttpClient<IModerationService, SightengineModerationService>(client =>
{
    client.BaseAddress = new Uri("https://api.sightengine.com/1.0/");
});



// registra o servico de autorizacao (sim ou nao)
// necessario para a [authorize] funcionar
builder.Services.AddAuthorization();

//Registra o serviço de controllers(mapeia automaticamente os controllers da pasta /Controllers)
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// redireciona http para https automaticamente
app.UseHttpsRedirection();

// ativa a autenticacao
app.UseAuthentication();

// ativa a autorizacao
app.UseAuthorization();

app.MapGet("/", () => "Hello World!");

//Mapeia as rotas definidas nos Controllers com os atributos [Route]: api/[controller]
app.MapControllers();

app.Run();
