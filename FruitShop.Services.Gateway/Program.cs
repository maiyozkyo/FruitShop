using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Values;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

//Add OcelotConfig
builder.Configuration.AddJsonFile("ocelotConfig.json", false, true);
builder.Services.AddOcelot(builder.Configuration);
var configuration = builder.Configuration;
var service = builder.Services;


service.AddCors(option =>
{
    option.AddPolicy("MyPolicy", builder =>
    {
        builder
        .AllowCredentials()
        .AllowAnyMethod()
        .AllowAnyHeader()
        .SetIsOriginAllowed(hostname => true)
        ;
    });
});

#region Authen
var jwtSerect = Encoding.ASCII.GetBytes(configuration.GetSection("JWTSerect").Value);
service.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtSerect),
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false,
        };
    });
#endregion
#region Service Config
service.Configure<CookiePolicyOptions>(options =>
{
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.Strict;
});
#endregion
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
app.MapControllers();
app.UseCors("MyPolicy");
app.UseAuthentication();
app.UseCookiePolicy();

await app.UseOcelot();
app.Run();
