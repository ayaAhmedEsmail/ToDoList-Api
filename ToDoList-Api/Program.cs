using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ToDoList_Api.Authorization;
using ToDoList_Api.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(opt => {
    opt.Filters.Add<PermissionBaseAuthorizationFilter>();
});

builder.Services.AddScoped<ToDoList_Api.Services.TokenService>();


builder.Services.AddAuthorization();
builder.Services.AddDbContext<ApplicationDBContext>(opt=> opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var JwtOptions = builder.Configuration.GetSection("Jwt").Get<JwtOptions>();
builder.Services.AddSingleton<JwtOptions>(opt => JwtOptions);
builder.Services.AddAuthentication().AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
    {
        opt.SaveToken = true;
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = JwtOptions.Issure,
            ValidateAudience = true,
            ValidAudience = JwtOptions.Audience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtOptions.SigningKey))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
