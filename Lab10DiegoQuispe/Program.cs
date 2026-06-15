using Hangfire;
using Lab10DiegoQuispe.Configurations;
using Lab10DiegoQuispe.Persistence.Models;
using Microsoft.EntityFrameworkCore;

var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
if (!string.IsNullOrEmpty(databaseUrl))
{
    var uri = new Uri(databaseUrl);
    var userInfo = uri.UserInfo.Split(':');
    var port = uri.Port > 0 ? uri.Port : 5432;  // ← fix aquí
    var connString = $"Host={uri.Host};Port={port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
    Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", connString);
}


var builder = WebApplication.CreateBuilder(args);

// Al inicio de Program.cs, antes del builder


builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

// Aplicar migraciones automáticamente



using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Console.WriteLine("Aplicando migraciones...");
    dbContext.Database.Migrate();
    Console.WriteLine("Migraciones aplicadas correctamente.");
}


app.UseSwagger();
app.UseSwaggerUI();



app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard("/hangfire");

app.MapControllers();

app.Run();