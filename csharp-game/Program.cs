//1: TODO: Add another endpoints, add containerization, Add additional funncionalities

using csharp_game.Data;
using csharp_game.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<GameContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ElephantSqlConnectionString")));

builder.Services.AddIdentity<ApplicationUser , IdentityRole>()
    .AddEntityFrameworkStores<GameContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<TokenService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if(app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

var validIssuer = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidIssuer");
var validAudience = builder.Configuration.GetValue<string>("JwtTokenSettings:ValidAudience");
var symmetricSecurityKey = builder.Configuration.GetValue<string>("JwtTokenSettings:SymmetricSecurityKey");

builder.Services.AddAuthorization();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ClockSkew = TimeSpan.Zero ,
            ValidateIssuer = true ,
            ValidateAudience = true ,
            ValidateLifetime = true ,
            ValidateIssuerSigningKey = true ,
            ValidIssuer = validIssuer ,
            ValidAudience = validAudience ,
            IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(symmetricSecurityKey)
            ) ,
        };
    });

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Run();
