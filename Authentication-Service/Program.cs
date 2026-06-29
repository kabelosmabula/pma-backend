using API.Service.Services;
using Authentication.Service.Helpers;
using Common;
using Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PMA.Application.Features.Users.Command.CreateAccount;
using PMA.Application.PracticePartitioner.Handler;
using PMA.Persistence;
using Scalar.AspNetCore;
using Services;
using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddOpenApi("v1", options => { options.AddDocumentTransformer<BearerSecuritySchemeTransformer>(); });

builder.Services.AddControllersWithViews(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer()));
});

var corsPolicy = "Domains";


builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
    {
        policy
          .AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod();
    });
});



var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddDbContext<PMADBContext>(opts => opts.UseNpgsql(connectionString));
Console.WriteLine(typeof(CreatePracticePractitionerCommandHandler).Assembly.FullName);
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
});


var secretKey = builder.Configuration.GetValue<string>("JwtSettings:SecretKey")
        ?? throw new InvalidOperationException("SecretKey is missing");
var key = Encoding.UTF8.GetBytes(secretKey);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;

    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var authHeader = context.Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                context.Token = authHeader["Bearer ".Length..].Trim();
            }
            return Task.CompletedTask;
        }
    };

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.FromMinutes(10)
    };
});

builder.Services.AddTransient<EmailService>();
builder.Services.AddTransient<JwtTokenService>();
builder.Services.AddTransient<Helper>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        string error = string.Empty;

        foreach (var modelState in context.ModelState)
        {
            if (modelState.Value.Errors.Count > 0)
            {
                error = modelState.Value.Errors[0].ErrorMessage;
                break;
            }
        }

        return new BadRequestObjectResult(Result<string>.Fail(error ?? "All fields are required"));
    };
});

var app = builder.Build();

app.MapOpenApi();


app.MapScalarApiReference("/docs", options =>
{
    options.WithTitle("Patient-Management-Application API Docs")
        .WithClassicLayout()
        .WithTheme(ScalarTheme.DeepSpace)
        .WithOpenApiRoutePattern("/openapi/{documentName}.json")
        .AddPreferredSecuritySchemes("Bearer")
        .AddHttpAuthentication("Bearer", auth =>
        {
            auth.Token = "";
        });
});

// var scalarUrl = "http://localhost:5030/docs";

// Process.Start(new ProcessStartInfo
// {
//     FileName = scalarUrl,
//     UseShellExecute = true
// });

app.UseHttpsRedirection();
app.UseCors(corsPolicy);
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();