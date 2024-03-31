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
            // Burada Signal da tanýmlanan MyHub sýnýfýna özel bir url tanýmlanýr.
            // Bu þekilde ilgili hub injeckt edilmiþ de olur. Böylece DI kullanýlarak istenilen yerde çaðrýlabilir.
            endpoints.MapHub<MyHub>("/myhub"); 
        });
        app.Run();
    }
}