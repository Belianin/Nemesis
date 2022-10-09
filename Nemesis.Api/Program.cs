using Nemesis.Api.Auth;
using Nemesis.Api.Lobbies;
using Nemesis.Api.Users;
using Nemesis.Api.Users.Sessions;
using Serilog;
using Serilog.Events;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateLogger();

try
{
    Log.Information("Starting web host");
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSingleton<LobbyService>();
    builder.Services.AddSingleton<IUserRepository, InMemoryUserRepository>();
    builder.Services.AddSingleton<ISessionRepository, InMemorySessionRepository>();
    builder.Services.AddControllers();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();
    builder.Services.AddAuthentication(opt =>
    {
        opt.DefaultScheme = AuthConsts.Scheme;
    }).AddScheme<SidAuthenticationSchemeOptions, SidAuthenticationHandler>(AuthConsts.Scheme, opt => {});

    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
    }
        app.UseCors(x => x
            .WithOrigins("http://localhost:3000")
            .AllowCredentials()
            .AllowAnyMethod()
            .AllowAnyHeader());

    //app.UseSerilogRequestLogging();
    app.UseStaticFiles();
    app.UseHttpLogging();
    app.UseDeveloperExceptionPage();
    app.UseRouting();

    app.UseWebSockets();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
        endpoints.MapFallbackToFile("index.html");
    });

    app.Run();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}