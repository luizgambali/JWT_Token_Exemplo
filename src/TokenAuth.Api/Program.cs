using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TokenAuth.Api;
using TokenAuth.Api.Model;
using TokenAuth.Api.Service;

var builder = WebApplication.CreateBuilder(args);

var key = Encoding.ASCII.GetBytes(Settings.Secret);                             //chave de segurança, convertida em array de bytes

// adiciona o serviço de autenticação ao container
builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;   //autenticação padrão
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;      //autenticação padrão
    }
).AddJwtBearer(x => 
    {
        x.RequireHttpsMetadata = false;                                         //requer https?
        x.SaveToken = true;                                                     //salvar o token no contexto http
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,                                    //validar a assinatura
            IssuerSigningKey = new SymmetricSecurityKey(key),                   //chave de segurança
            ValidateIssuer = false,                                             //validar o emissor
            ValidateAudience = false,                                           //validar o receptor
        };
    }
);

// adiciona as politicas de acesso
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("User", policy => policy.RequireRole("User"));
    options.AddPolicy("Viewer", policy => policy.RequireRole("Viewer"));
    options.AddPolicy("Audit", policy => policy.RequireRole("Audit"));
});

var app = builder.Build();

// habilita o uso de autenticação, tem que colocar nessa ordem senão não funciona
app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/login", (User model) => 
{
    var (code, user, token) = LoginService.Login(model);

    if (code == 401) 
        return Results.Unauthorized();

    return Results.Ok(new { user = user, token = token });                         
});

app.MapGet("/admin", () => "Você é um admin, então pode ver essa mensagem!").RequireAuthorization("Admin");
app.MapGet("/user", () => "Você é um user, então pode ver essa mensagem!").RequireAuthorization("User");
app.MapGet("/viewer", () => "Você é um usuario com permissão de visualização, então pode ver essa mensagem!").RequireAuthorization("Viewer");
app.MapGet("/audit", () => "Você é um auditor, então pode ver essa mensagem!").RequireAuthorization("Audit");
app.MapGet("/protected", () => "Você precisa estar autenticado para ver essa mensagem!").RequireAuthorization();
app.MapGet("/anonymous", () => "Você não precisa estar autenticado para ver essa mensagem").AllowAnonymous();

app.Run();
