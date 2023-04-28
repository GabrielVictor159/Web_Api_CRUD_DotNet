using Web_Api_CRUD.Infraestructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Web_Api_CRUD;
using Web_Api_CRUD.Model.Enums;
using Autofac.Extensions.DependencyInjection;
using Web_Api_CRUD.Repository;
using Autofac;

var builder = WebApplication.CreateBuilder(args);

var containerBuilder = new ContainerBuilder();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
containerBuilder.RegisterModule(new AutoFacRepositories());
var container = containerBuilder.Build();
var serviceProvider = new AutofacServiceProvider(container);
builder.Services
.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var key = Encoding.ASCII.GetBytes(Settings.Secret);
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Cliente", policy => policy.RequireRole(Policies.User));
});
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(e =>
{
    e.RequireHttpsMetadata = false;
    e.SaveToken = true;
    e.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
    };
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

app.Run();
