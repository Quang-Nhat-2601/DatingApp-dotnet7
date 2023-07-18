using System.Text;
using API.Data;
using API.Extensions;
using API.Interfaces;
using API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityService(builder.Configuration);
var app = builder.Build();

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod()
                              .WithOrigins("https://localhost:4200"));

app.UseAuthentication();
app.UseAuthorization();
// Configure the HTTP request pipeline.
app.MapControllers();

app.Run();
