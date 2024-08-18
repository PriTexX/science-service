using Science.Auth;
using Science.Config;
using Science.DB;
using Science.Features.DataLoader;
using Science.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.InitCfg();

builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddMosPolytechAuth();
builder.Services.AddDB();
builder.AddLogging();

builder.Services.AddScoped<DataLoaderService>();

var app = builder.Build();

app.UseCors(corsPolicyBuilder =>
{
    corsPolicyBuilder.AllowAnyOrigin();
    corsPolicyBuilder.AllowAnyHeader();
});

app.UseHttpsRedirection();

app.UseRouting();

app.UseMosPolytechAuth();

app.MapControllers();

app.Run();
