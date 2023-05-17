using Data;
using FluentValidation.AspNetCore;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Writers;
using Api.Apps.AdminApi.Dtos.BrandDtos;
using Swashbuckle.AspNetCore.Swagger;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Api.Profiles;
using Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShopDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<ShopDbContext>();
// Add services to the container.
builder.Services.AddScoped<JwtService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("admin_v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Shop Api for Admins"
    });
	opt.SwaggerDoc("user_v1", new Microsoft.OpenApi.Models.OpenApiInfo
	{
		Version = "v1",
		Title = "Shop Api for Users"
	});
	opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		In = ParameterLocation.Header,
		Description = "Please enter a valid token",
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		BearerFormat = "JWT",
		Scheme = "Bearer"
	});
	opt.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});
});
builder.Services.AddFluentValidationRulesToSwagger();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<BrandDtoValidator>();
builder.Services.AddAutoMapper(opt =>
{
    opt.AddProfile(new MapProfile());
});
builder.Services.AddAuthentication(opt =>
{
	opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
	{
		IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value)),
		ValidIssuer = builder.Configuration.GetSection("JWT:Issuer").Value,
		ValidAudience = builder.Configuration.GetSection("JWT:Audience").Value
	};
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(opt =>
    {
        opt.SwaggerEndpoint("/swagger/admin_v1/swagger.json", "admin_v1");
		opt.SwaggerEndpoint("/swagger/user_v1/swagger.json", "user_v1");
	});
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
