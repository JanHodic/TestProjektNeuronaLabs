using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Neurona.Health.Api.Api.GraphQL.Mutations;
using Neurona.Health.Api.Api.GraphQL.Queries;
using Neurona.Health.Api.Application;
using Neurona.Health.Api.Infrastructure.Data;
using GraphQL.Server.Ui.Voyager;
using HotChocolate.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Neurona.Health.Api.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

/*builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));*/

var jwtSection = builder.Configuration.GetSection("Jwt");
var jwtKey = Encoding.UTF8.GetBytes(jwtSection["Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSection["Issuer"],
            ValidAudience = jwtSection["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey),
            ClockSkew = TimeSpan.FromMinutes(1)
        };
    });

builder.Services.AddAuthorization();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddFiltering()
    .AddSorting()
    .AddAuthorization()
    .ModifyRequestOptions(o =>
    {
        o.IncludeExceptionDetails = builder.Environment.IsDevelopment();
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();

    if (!db.Patients.Any())
    {
        db.Patients.Add(new Patient
        {
            FullName = "Jan Novák",
            Age = 45,
            LastDiagnosis = "Hypertenze",
            DiagnosticEntries = new List<DiagnosticEntry>
            {
                new() { Diagnosis = "Hypertenze I. stupně", RecordedAt = DateTime.UtcNow.AddDays(-7) },
                new() { Diagnosis = "Kontrola – beze změny", RecordedAt = DateTime.UtcNow }
            }
        });

        db.SaveChanges();
    }
}

app.UseAuthentication();
app.UseAuthorization();

app.MapGraphQL("/graphql").WithOptions(new GraphQLServerOptions
{
    Tool = { Enable = true }
});

app.MapPost("/auth/token", () =>
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "demo-user"),
            new Claim(ClaimTypes.Name, "demo-user"),
            new Claim(ClaimTypes.Role, "Doctor")
        };

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Results.Ok(new { access_token = tokenString });
    })
    .WithName("GetDemoToken");

app.UseGraphQLVoyager("/voyager", new VoyagerOptions
{
    GraphQLEndPoint = "/graphql"
});

app.Run();