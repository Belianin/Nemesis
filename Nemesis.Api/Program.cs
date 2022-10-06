using Nemesis.Api.Lobbies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<LobbyService>();
builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors(x => x
        .WithOrigins("http://localhost:3000")
        .AllowCredentials()
        .AllowAnyMethod()
        .AllowAnyHeader());
}

app.MapControllers();
app.UseWebSockets();
app.UseStaticFiles();
app.UseDefaultFiles();

app.Run();