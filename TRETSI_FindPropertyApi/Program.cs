using API.Middleware;
using CompanyEmployees;
using CompanyEmployees.JwtFeatures;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TR_ETSI_PropertyFinderApi.DBContext;
using TR_ETSI_PropertyFinderApi.Interfaces;
using TR_ETSI_PropertyFinderApi.Models;

using TRETSIPropertyFinderApi.DTO;

using TRETSIPropertyFinderApi.Interfaces;
using TRETSIPropertyFinderApi.Services;

var builder = WebApplication.CreateBuilder(args);
var mongoDbSettings = builder.Configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
(
    mongoDbSettings.ConnectionString, mongoDbSettings.DatabaseName
);
//builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
//builder.Services.AddSingleton<IDatabaseSettings>(x => x.GetRequiredService<IOptions<MongoDbConfig>>().Value);

builder.Services.Configure<DatabaseSettings>(
     builder.Configuration.GetSection(nameof(DatabaseSettings)));

builder.Services.AddSingleton<IPropertyFinderDatabaseSettings>(provider =>
    provider.GetRequiredService<IOptions<DatabaseSettings>>().Value);
builder.Services.AddTransient<IUserContext, DbContext>();
builder.Services.AddScoped<IPropertyKind, PropertyService>();
//builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddAutoMapper(typeof(MappingProfile));
//builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});
builder.Services.AddScoped<JwtHandler>();




var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(policy => {
    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(new string[] { "http://localhost:4200", "http://localhost:6512" });
});
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();

