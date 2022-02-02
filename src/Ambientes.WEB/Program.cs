using Ambientes.WEB;

var builder = WebApplication.CreateBuilder();
builder.UseStartup<Startup>(builder.Environment);
