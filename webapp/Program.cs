using System.Reflection;
using Application;
using Data;
using Infrastructure;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var constring = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container
builder.Services.AddAutoMapperConfig();
builder.Services.AddData(constring);
builder.Services.AddInfrastructure();
builder.Services.AddApplication();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "CollegeManagementApi", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme,
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
                },
                new List<string>()
        }
    });
});

var tokenKey = builder.Configuration["TokenKey"];
if (string.IsNullOrEmpty(tokenKey))
{
    throw new Exception("TokenKey is null or empty");
}

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey)),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

// Add Authorization with custom policies
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("EsAdministrador", policy =>
        policy.RequireClaim("Role", "Administrador"));

    options.AddPolicy("EsUsuario", policy =>
        policy.RequireClaim("Role", "Usuario"));

    options.AddPolicy("EsAdminOUsuario", policy =>
        policy.RequireAssertion(context =>
        {
            var roles = context.User.FindAll("Role").Select(r => r.Value);
            return roles.Contains("Administrador") || roles.Contains("Usuario");
        }));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(config =>
    {
        config.SwaggerEndpoint("/swagger/v1/swagger.json", "CollegeManagementApi v1");
        config.RoutePrefix = string.Empty;
    });
}

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().WithOrigins("*"));
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();


