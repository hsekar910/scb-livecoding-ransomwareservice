using ransomware_webapi.Models;
using ransomware_webapi.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<RansomwareDBSettings>(
    builder.Configuration.GetSection("RansomwareDBCollection")
);

builder.Services.AddSingleton<RansomwareService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS policy
// Read allowed origins from configuration
var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
            builder =>
            {
                if (allowedOrigins != null)
                {
                    builder.WithOrigins(allowedOrigins) // Replace with your allowed origin
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                }
            });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// Use CORS middleware
app.UseCors("AllowSpecificOrigin");

app.UseAuthorization();

app.MapControllers();

app.Run();
