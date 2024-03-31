using SignalRExample.Hubs;
using SignalRExample.Services;

// ReSharper disable CommentTypo

namespace SignalRExample;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR();
        builder.Services.AddTransient<SignalRService>();
        builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.AllowAnyMethod()
                                                                                     .AllowAnyHeader()
                                                                                     .AllowCredentials()
                                                                                     .SetIsOriginAllowed(origin => true)));

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors();
        app.UseRouting();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            // Burada Signal da tan�mlanan MyHub s�n�f�na �zel bir url tan�mlan�r.
            // Bu �ekilde ilgili hub injeckt edilmi� de olur. B�ylece DI kullan�larak istenilen yerde �a�r�labilir.
            endpoints.MapHub<MyHub>("/myhub"); 
        });
        app.Run();
    }
}