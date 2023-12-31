﻿using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UsuariosApi.Authorization;
using UsuariosApi.Data;
using UsuariosApi.Models;
using UsuariosApi.Services;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var connString = builder.Configuration.GetConnectionString("UsuarioConnection");

        builder.Services.AddDbContext<UsuarioDbContext>
        (opts =>
        {
            opts.UseMySql(connString, ServerVersion.AutoDetect(connString));
        });

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("0nxs9wsjne09hs90dhsd90hnsd")),
                ValidateAudience = false,
                ValidateIssuer = false,
                ClockSkew = TimeSpan.Zero
            };
        });

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("IdadeMinima", policy => policy.AddRequirements(new IdadeMinima(18)));
        });

        builder.Services.AddSingleton<IAuthorizationHandler, IdadeAuthorization>();
        
        builder.Services
            .AddIdentity<Usuario, IdentityRole>()
            .AddEntityFrameworkStores<UsuarioDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddScoped<UsuarioService>();
        builder.Services.AddScoped<TokenService>();
        
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();


        var app = builder.Build();
        
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
    }
}