using Web_Api_Autores;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);

var app = builder.Build();

var servicioLogger = (ILogger < Startup >) app.Services.GetService(typeof(ILogger<Startup>));
startup.Configurate(app, app.Environment, servicioLogger);

startup.Configurate(app, app.Environment);


app.Run();
